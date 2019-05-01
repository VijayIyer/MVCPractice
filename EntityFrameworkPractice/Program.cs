using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace EntityFrameworkPractice
{
    class Program
    {
        static void Main(string[] args)
        {
            //var task = PerformDatabaseOperations();
            //while (task.Status != TaskStatus.RanToCompletion)
            //{
            //    Console.WriteLine("Query execution is going on ");
            //}

            //Console.WriteLine("Query execution completed");
            // LinqQueries();
            using (var context = new AdventureWorksEntities())
            {
             foreach (var c in ReturnCustomerAsync().Result)
                {
                    Console.WriteLine(c.CustomerID);
                }
            }
            // inserting into products table
            // insertproductrecord();
            //var product = findproduct(); 
            //updateproductrecord(product.ProductID);
            //removeproductrecord(product.ProductID);
        }

        public static void insertproductrecord()
        {
            using (var context = new AdventureWorksEntities())
            {
                var product = new Product()
                {
                    ProductID = context.Products.AsEnumerable<Product>().Last().ProductID + 1,
                    Name = "New Product 16",
                    ProductNumber = "156",
                    Color = "Red",
                    StandardCost = new decimal(1000.97),
                    ListPrice = new decimal(2000),
                    Size = "Large",
                    ThumbNailPhoto = new byte[100],
                    ThumbnailPhotoFileName = "FileName",
                    ProductCategory = context.ProductCategories.First<ProductCategory>(),
                    ProductModel = context.ProductModels.First()
                };

                context.Products.Add(product);
                context.SaveChanges();
                context.Database.Log = x => System.Diagnostics.Debug.WriteLine(x);
            }
        }
        public static void updateproductrecord(int productid)
        {
            using (var context = new AdventureWorksEntities())
            {
                var product = context.Products.Find(productid);
                product.Name = "Steve Bruce Buffer fasa";
                context.SaveChanges();

            }
        }

        public static Product findproduct()
        {
            var product = new Product();
            using (var context = new AdventureWorksEntities())
            {
                product  = context.Products.Find(context.Products.AsEnumerable<Product>().Last().ProductID);
             }
            return product;
        }
        public static void removeproductrecord(int productid)
        {
            using (var context = new AdventureWorksEntities())
            {
                var product = context.Products.Find(productid);
 context.Products.Remove(product);
                context.SaveChanges();
            }
        }

        public static void LinqQueries()
        {
            using (var context = new AdventureWorksEntities())
            {
                
           
               
                #region Using Stored Procedure

                foreach (var c in context.Customers)
                {
                   
                    var CustomerAddresses  = context.GetCustomeAddressById(c.CustomerID);
                    foreach (var ca in CustomerAddresses)
                    {
                        Console.WriteLine(ca.CustomerId + " " + ca.FirstName + " " + ca.LastName + " " + ca.CompanyName +" " + ca.City + " " + ca.CountryRegion);
                    }
                }
                   
                #endregion

                #region module 6 lab 1

                Console.WriteLine("Products with Listprice more than average of unit price of all products");
                var prodlphigherthansp = from p in context.Products
                                  where p.ListPrice > context.SalesOrderDetails.Average(a => a.UnitPrice)
                                  select new
                                  {

                                      ProductId = p.ProductID,
                                      Name = p.Name

                                  };


                foreach (var p in prodlphigherthansp)
                {
                    Console.WriteLine(p.Name + " ");
                }

                #endregion
                #region module 6 lab 2
                Console.WriteLine("Products with Listprice more than 100 and Unit Price less than 100");

                var innerquery = context.SalesOrderDetails.Where(a => a.UnitPrice < 100).Select(a => a.ProductID);
                var q = from t1 in context.Products
                        where innerquery.Contains(t1.ProductID) && t1.ListPrice > 100
                        select new 
                        {
                            t1.ProductID,t1.Name,t1.ListPrice
                        };

                foreach (var p in q)
                {
                    Console.WriteLine( p.ProductID + " " +p.Name + " " + p.ListPrice);
                }

                #endregion
                #region moduel 6 lab 3 
                Console.WriteLine("Products along with average selling price of each");



                var productsmorethan = context.Products.GroupJoin(context.SalesOrderDetails, a => a.ProductID, b => b.ProductID, (a, b) => new { ProductId = a.ProductID, Name = a.Name, StandardCost = a.StandardCost, ListPrice = a.ListPrice, AverageListPrice = b.Average(c => c.UnitPrice) }).Where(a => a.AverageListPrice > 0 );

            
                int count = 0;
                foreach (var p in productsmorethan)
                {

                    Console.WriteLine(++count+" " + p.ProductId + " " + p.Name + " "+ p.StandardCost + " " + p.AverageListPrice );
                }

                #endregion
                #region module 6 lab 4 
                Console.WriteLine("Products having Standard Cost greater than average selling price");
                var productsgroupby = context.Products.GroupJoin(context.SalesOrderDetails, a => a.ProductID, b => b.ProductID, (a, b) => new { ProductId = a.ProductID, Name = a.Name, StandardCost = a.StandardCost, ListPrice = a.ListPrice, AverageListPrice = b.Average(c => c.UnitPrice) }).Where(a => a.StandardCost > a.AverageListPrice);


               foreach (var p in productsgroupby)
                {

                    Console.WriteLine(++count + " " + p.ProductId + " " + p.Name + " " + p.StandardCost + " " + p.AverageListPrice);
                }
                #endregion
                #region module 6 lab 2-1

                Console.WriteLine("Customer info for all salesorders");


                foreach(var c in context.SalesOrderHeaders)
                {
                    var udf = (from s in context.SalesOrderHeaders
                               join d in context.ufnGetCustomerInformation(c.CustomerID)
                               on s.CustomerID equals d.CustomerID
                               orderby s.SalesOrderID
                               select new
                               {
                                   s.SalesOrderID,
                                   d.CustomerID,
                                   d.FirstName,
                                   d.LastName,
                                   s.TotalDue
                               }).ToList();
                    foreach (var u in udf)
                    {
                        Console.WriteLine(u.CustomerID + " " + u.SalesOrderID + " " + u.FirstName + " " + u.LastName + " " + u.TotalDue);

                    }
                }


                #endregion

                #region module 6 lab 2-2

                Console.WriteLine("Customer info for all salesorders");

                //foreach (var c in context.Customers)
                //{
                //    var udf2 = (from a in context.Addresses
                //               join ca in context.CustomerAddresses

                //               on a.AddressID equals ca.AddressID
                             
                //               orderby ca.CustomerID
                //               select new
                //               {
                //                   CustomerId = context.ufnGetCustomerInformation(c.CustomerID),
                //                   FirstName = context.ufnGetCustomerInformation(c.CustomerID)
                //                   a.AddressLine1,
                //                   a.City
                //               }).ToList();
                //    foreach (var u in udf2)
                //    {
                //        Console.WriteLine(u.CustomerId + " " + u.FirstName + " " + u.LastName + " " + u.AddressLine1 + " " + u.City);
                //    }
                    
                //}


                #endregion


                var productslessthan = from p in context.Products
                                       join s in context.SalesOrderDetails
                                       on p.ProductID equals s.ProductID into grp
                                       from g in grp
                                      where p.StandardCost > grp.Average(a => a.UnitPrice)
                                      select new
                                       {
                                           p.ProductID,
                                           p.Name,
                                           p.StandardCost,
                                           p.ListPrice,
                                           averagesellingprice = grp.Average(a => a.UnitPrice)
                                       };

                foreach (var p in productslessthan)
                {
                    Console.WriteLine(p.ProductID + " " + p.Name + " " + p.StandardCost + " ");
                }



                #region module 7 lab 1-1

                Console.WriteLine("Product with Product Model Summary");
                var productmodelcatalogdescription = context.Products.Join(context.vProductModelCatalogDescriptions,a=> a.ProductModelID,b => b.ProductModelID, (a,b) => new { ProductId = a.ProductID, Name  = a.Name , ModelName = b.Name , Summary = b.Summary});

                foreach (var p in productmodelcatalogdescription)
                {
                    Console.WriteLine(p.ProductId + " " + p.Name + " "+p.ModelName + " "+p.Summary);
                }

                #endregion
                #region module 7 lab 1-2
                #endregion
              #region everything else
                // customers along with orders and product - query syntax

                var customerlist = from c in context.Customers
                                   join d in context.SalesOrderHeaders
                                    on c.CustomerID equals d.CustomerID
                                   join e in context.SalesOrderDetails 
                                   on d.SalesOrderID equals e.SalesOrderID
                                  select new
                                  {
                                      FirstName = c.FirstName,
                                      SalesOrderId = d.SalesOrderID,
                                      ProductId = e.ProductID
                                  };
                foreach (var customer in customerlist)
                {
                Console.WriteLine(customer.FirstName + "\t"+ customer.SalesOrderId + "\t"+ customer.ProductId +"\n");
                }

               

                // customers along with orders and product - method syntax

                var customermethodsyntax = context.Customers
                     .Join(context.SalesOrderHeaders, a => a.CustomerID, b => b.CustomerID, (a, b) => new { FirstName = a.FirstName, SalesOrderId = b.SalesOrderID })
                    .Join(context.SalesOrderDetails, a => a.SalesOrderId, b => b.SalesOrderID, (a, b) => new { FirstName = a.FirstName,SalesOrderId = b.SalesOrderID,ProductId = b.ProductID });
                foreach (var customer in customermethodsyntax)
                {
                    Console.WriteLine(customer.FirstName + "\t" + customer.SalesOrderId + "\t"+ customer.ProductId+"\n");
                }




                // salesorderheader with salesorderdetails query syntax

                var salesorderheaders = from sh in context.SalesOrderHeaders
                                        join sd in context.SalesOrderDetails
                                        on sh.SalesOrderID equals sd.SalesOrderID
                                        select new
                                        {
                                            SalesOrderNumber = sh.SalesOrderNumber,
                                            productId = sd.ProductID
                                        };
                foreach (var sh in salesorderheaders)
                {
                    Console.WriteLine(sh.SalesOrderNumber + ":" + sh.productId + "\n");
                }

                // salesorderheader with salesorderdetails method syntax

                // salesorderheader with salesorderdetails - group join using 'into'

               var salesorderheadergroup = from sh in context.SalesOrderHeaders
                                    join sd in context.SalesOrderDetails
                                    on sh.SalesOrderID equals sd.SalesOrderID into sdj
                                    from s in sdj
                                    select new
                                    {
                                        SalesOrderNumber = sh.SalesOrderNumber,
                                        Products  = sdj
                                        };
                foreach (var sh in salesorderheadergroup)
                {
                    Console.WriteLine("SalesOrderNumber" + sh.SalesOrderNumber + ":" );
                    foreach (var salesorderdetail in sh.Products)
                    {
                        Console.WriteLine("\t SalesOrderProductId" + salesorderdetail.ProductID);
                    }
                }


                //customers with salesorders groupjoin

                var customerordergroup = context.SalesOrderHeaders.GroupJoin(context.SalesOrderDetails, a => a.SalesOrderID, b => b.SalesOrderID, (a,b) => new { CustomerId = a.SalesOrderNumber, salescount = b.Count(), SalesOrderDetai = b }).Where(a => a.salescount>0);
                foreach (var customerorder in customerordergroup)
                {
                    Console.WriteLine(customerorder.CustomerId +" "+ customerorder.salescount);

                    foreach (var salesorder in customerorder.SalesOrderDetai)
                    {

                        Console.WriteLine("\t" + salesorder.ProductID);

                    }
                }

                // products per productmodel group by using query syntax
                var productmodelperproductquerysyntax = (from c in context.Products
                                             group c by c.ProductModelID into cpm
                                             select new { ProductModelId = cpm.Key, Products = cpm }).Take(1);
                foreach (var model in productmodelperproductquerysyntax)
                {
                    Console.WriteLine("Product model :" + model.ProductModelId + " Count :" + model.Products.Count());
                    foreach (var product in model.Products)
                    {
                        Console.WriteLine(" - " + product.Name);
                    }

                }

                // products per productmodel group by using method syntax

                var productmodelperproduct = context.Products.GroupBy(a => a.ProductModelID).Take(1);

                foreach(var model in productmodelperproduct)
                {
                    Console.WriteLine("Product model :" + model.Key + " Count :" + model.Count());
                    foreach(var product in model)
                    {
                        Console.WriteLine(" - "+ product.Name);
                    }
                    
                }


                // grouped join  query syntax
                var groupsalesorderheaders = from sh in context.SalesOrderHeaders
                                    join sd in context.SalesOrderDetails
                                    on sh.SalesOrderID equals sd.SalesOrderID into sdj
                                    orderby sh.SalesOrderNumber descending
                                    select new
                                    {
                                        SalesOrderNumber = sh.SalesOrderNumber,
                                        p = from s in sdj
                                            orderby s.ProductID descending
                                            select s
                                    };
                foreach (var sh in groupsalesorderheaders)
                {
                    Console.WriteLine(sh.SalesOrderNumber + ":" + "\n");

                    foreach (var pi in sh.p)
                    {
                        Console.WriteLine("\t"+pi.ProductID+"\n");


                    }

                }

                //grouped join method syntax




                // customers with no orders
                var groupcustomers = from c in context.Customers
                                     join s in context.SalesOrderHeaders
                                     on c.CustomerID equals s.CustomerID into sdj
                                     from s in sdj.DefaultIfEmpty()
                                     where s == null
                                     select new
                                     {
                                         CustomerId = c.CustomerID,
                                         SalesOrderHeader = s != null ? s.SalesOrderNumber : string.Empty
                                     };
                foreach (var gc in groupcustomers)
                {
                    Console.WriteLine(gc.CustomerId + ":" + gc.SalesOrderHeader);
                }


                // customers with no address
                var customerwithoutaddress = from c in context.Customers
                                             join ca in context.CustomerAddresses
                                             on c.CustomerID equals ca.CustomerID into cca
                                             from ccai in cca.DefaultIfEmpty()
                                            where ccai == null
                                     select new
                                     {
                                         CustomerId = c.CustomerID,
                                        
                                     };
                count = 1;
                foreach (var gc in customerwithoutaddress)
                {
                    
                    Console.WriteLine(count.ToString()+":"+ gc.CustomerId);
                    count += 1;
                }

                // grouping of product as per productmodel

                var productmodel = (from p in context.Products
                                   group p by p.ProductModelID into pm
                                   select new
                                   {
                                       productmodelid = pm.Key,
                                       averagelistprice = pm.Average(a => a.ListPrice),
                                       productidcount = pm.Count(),
                                       standardcost = pm.Max(a => a.StandardCost)
                                   }).Take(5);

                foreach (var gc in productmodel)
                {

                    Console.WriteLine(gc.productmodelid + " "+ gc.averagelistprice + " "+gc.productidcount + " " + gc.standardcost);
                    
                }


                var products = context.Products;

                Decimal averageListPrice =
                    products.Average(product => product.ListPrice);

                Console.WriteLine("The average list price of all the products is ${0}",
                    averageListPrice);

                // Raw SQL Queries

                #endregion
             
            }
        }
        public static async Task PerformDatabaseOperations()
        {
            using (var db = new AdventureWorksEntities())
            {
                // Create a new blog and save it
                try
                {

                 
                    db.Products.Add(new Product
                    {
                        
                        ProductID = db.Products.Last().ProductID + 1,
                        Name = "New Product 3580",
                        ProductNumber = "3580",
                        Color = "Reddish",
                        StandardCost = new decimal(1000.97),
                        ListPrice = new decimal(2000),
                        Size = "Large",
                        ThumbNailPhoto = new byte[100],
                        ThumbnailPhotoFileName = "FileName",
                        ProductCategory = db.ProductCategories.First<ProductCategory>(),
                        ProductModel = db.ProductModels.First()
                    });
                }
                catch(Exception e)
                {
                    Console.WriteLine("No able to add Product");
                }

                Console.WriteLine("Adding product to Context");

                try
                {
                    await db.SaveChangesAsync();
                }
                catch
                {
                    Console.WriteLine("Underlying data bad");
                }
               

               
            }
        }

        public static async Task<List<Customer>> ReturnCustomerAsync()
        {

            var context = new AdventureWorksEntities();
            return await context.Customers.ToListAsync();
                               
        }

    }

}
