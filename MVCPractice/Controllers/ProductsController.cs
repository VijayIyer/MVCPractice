using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Data.Entity;
using System.Linq.Dynamic;
namespace MVCPractice.Controllers
{
    public class ProductsController : Controller
    {
        private AdventureWorksEntities context = new AdventureWorksEntities();

        [OutputCache(Duration = 200,Location =System.Web.UI.OutputCacheLocation.Client,VaryByParam ="page")]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page,string filterstring)
        {
            int pageSize = 10;
            ViewBag.CurrentSort = sortOrder;

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;
            ViewBag.Filter = filterstring;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            var search = string.IsNullOrEmpty(searchString) ? "" : searchString;
                var Products = context.Products
                     .Include("ProductCategory")
                      .Include("ProductModel").AsParallel<Product>()
                      .Where(s => s.Name.Contains(search));
            switch (sortOrder)
                {
                    case "name_desc":
                        Products = Products.OrderByDescending(s => s.Name);
                        break;
                    case "Date":
                        Products = Products.OrderBy(s => s.SellStartDate);
                        break;
                    case "date_desc":
                        Products = Products.OrderByDescending(s => s.SellStartDate);
                        break;
                    default:
                        Products = Products.OrderBy(s => s.Name);
                        break;
                }
            int pageNumber = (page ?? 1);
            if (Request.IsAjaxRequest())
            {
                return View("ProductsTable", Products.ToPagedList(pageNumber, pageSize));
            }
            
            return View(Products.ToPagedList(pageNumber, pageSize));
        }
        [OutputCache(Duration = 300,Location =System.Web.UI.OutputCacheLocation.Client)]
        [HttpGet]
        public ActionResult Create()
        {
               ViewBag.ProductCategoryId = new SelectList((from c in context.ProductCategories
                                                        where c.ParentProductCategoryID == null
                                                        select c).ToList()
                                                            , "ProductCategoryId", "Name");

           

            ViewBag.ProductModelId = new SelectList(context.ProductModels.ToList(), "ProductModelId", "Name");
                ViewBag.Colors = new SelectList(context.Products.Where(a => a.Color != null).Select(a => a.Color).Distinct().ToList());
           
        return View();
        }
        [HttpGet]
        public ActionResult GetSubCategory(int SelectedProductCategory)
        {
            var SubCategory = from p in context.ProductCategories
                              where p.ParentProductCategoryID == SelectedProductCategory
                              select new
                              {
                                  Id = p.ProductCategoryID,
                                  Name = p.Name
                              };

            return Json(SubCategory.ToList(),JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetProductCategories(string SelectedCountry)
        {
            var SelectParentCategoryId = context.ProductCategories.Where(a => a.Name == SelectedCountry).Select(a => a.ProductCategoryID).First();
            IEnumerable<SelectListItem> ProductCategories = new SelectList((from c in context.ProductCategories
                                                                            where c.ParentProductCategoryID == SelectParentCategoryId
                                                                            select c)
                                                            , "ProductCategoryId", "Name");

            return Json(ProductCategories.ToList(), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ActionName("Create")]
        public ActionResult CreatePost(FormCollection F)
        {
            var producttocreate = new Product();
            try
            {
                UpdateModel(producttocreate);
                context.Products.Add(producttocreate);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                    ViewBag.Colors = new SelectList(context.Products.Select(a => a.Color).Distinct().ToList(), F["Color"]);
                    ViewBag.ProductCategoryId = new SelectList((from c in context.ProductCategories
                                                                where c.ParentProductCategoryID == null
                                                                select c).ToList()
                                                        , "ProductCategoryId", "Name", F["ProductCategoryId"]);

                    ViewBag.ProductModelId = new SelectList(context.ProductModels.ToList(), "ProductModelId", "Name", F["ProductModelId"]);
                }
                return View();
            }

        [OutputCache(Duration = 300,VaryByParam = "Id")]
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            Product requiredproduct = new Product();
            requiredproduct = context.Products.Find(Id);
            ViewBag.ProductCategoryId = new SelectList((from c in context.ProductCategories
                                                        where c.ParentProductCategoryID == null
                                                        select c).ToList()
                                                        , "ProductCategoryId", "Name");



            ViewBag.ProductModelId = new SelectList(context.ProductModels.ToList(), "ProductModelId", "Name");
            ViewBag.Colors = new SelectList(context.Products.Where(a => a.Color != null).Select(a => a.Color).Distinct().ToList(),requiredproduct.Color);

            
            return View(requiredproduct);
        }
        [HttpPost]
        public ActionResult Edit(Product product)
        {

            try {
               
                    var required = context.Products.Find(product.ProductID);
                    UpdateModel(required);
                    context.SaveChanges();
                    return RedirectToAction("Index");
              }
            catch(Exception e)
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var required = context.Products.Find(id);
                context.Products.Remove(required);
                context.SalesOrderDetails.RemoveRange(context.SalesOrderDetails.Where(a => a.ProductID == id));
                context.SaveChanges();
                 return RedirectToAction("Index");
        }

        // Create Products Form

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                context.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}