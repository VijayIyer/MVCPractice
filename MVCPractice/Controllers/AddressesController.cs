using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCPractice;
using Newtonsoft.Json;

namespace MVCPractice.Controllers
{
    [MVCPractice.Filters.ActionSpeedProfiler]
    public class AddressesController : Controller
    {
        private AdventureWorksEntities db = new AdventureWorksEntities();

        // GET: Addresses

        public ActionResult MainView()
        {
            db.Configuration.LazyLoadingEnabled = false;
            db.Configuration.ProxyCreationEnabled = false;
            return View();

        }

        public ActionResult Index()
        {
            db.Configuration.LazyLoadingEnabled = false;
            db.Configuration.ProxyCreationEnabled = false;
            return View();

        }

        public JsonResult GetAddresses()
        {
            var Addresses = db.Addresses.ToList().Select(x => new Address
            {
                AddressID = x.AddressID,
                AddressLine1 = x.AddressLine1,
                City= x.City,
                StateProvince = x.StateProvince,
                CountryRegion = x.CountryRegion 
            });
            return Json(Addresses,JsonRequestBehavior.AllowGet);
       }
        //  GET: Addresses/Edit/5
        public JsonResult GetAddressDetails(int? id)
        {

         //   Address address = db.Addresses.Find(id);
            var address = db.Addresses.ToList().Select(x =>
            new {
                AddressID = x.AddressID,
                AddressLine1 = x.AddressLine1,
                AddressLine2=x.AddressLine2,
                City = x.City,
                StateProvince = x.StateProvince,
                CountryRegion = x.CountryRegion,
                PostalCode=x.PostalCode
            }).Where(a => a.AddressID==id).First();
            return Json(address,JsonRequestBehavior.AllowGet);

        }
        public ActionResult Shared(string ViewName)
        {
            if (string.IsNullOrEmpty(ViewName))
            {
                ViewName = "Index";
            }
            ViewBag.ViewName = ViewName;
            return View("Shared");
        }

        // GET: Addresses/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Address address = await db.Addresses.FindAsync(id);

            if (address == null)
            {
                return HttpNotFound();
            }
            if(Request.IsAjaxRequest())
            { return PartialView(address); }
            return View(address);
        }

        // GET: Addresses/Create
        public ActionResult Create()
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView();
            }
            return View();
        }

        // POST: Addresses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
      //  [ValidateAntiForgeryToken]
        public JsonResult Create(Address address)
        {
            if (ModelState.IsValid)
            {
                db.Entry(address).State = EntityState.Added;
                try
                {
                    
                    db.SaveChanges();
                    return Json(new { success = true, message = "Changes to the User were saved successfully" });
                }
                catch (Exception e)
                {
                    return Json(new { error = true, message = "There were problems in saving changes for given entity" });
                }
            }

            return Json(new { error = true, message = "There were problems in saving changes for given entity" });
        }

      ////  GET: Addresses/Edit/5
      //  public async Task<ActionResult> Edit(int? id)
      //  {
      //      if (id == null)
      //      {
      //          return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      //      }
      //      Address address = await db.Addresses.FindAsync(id);
      //      if (address == null)
      //      {
      //          return HttpNotFound();
      //      }
      //      if (Request.IsAjaxRequest())
      //      {
      //          return PartialView(address);
      //      }
      //      return View(address);
            
      //  }

        // POST: Addresses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
      //  [ValidateAntiForgeryToken]
        public JsonResult Edit(Address address)
        {
        
            if (ModelState.IsValid)
            {
                db.Entry(address).State = EntityState.Modified;
                try
                {
                    db.SaveChanges();
                    return Json(new { success = true, message = "Changes to the User were saved successfully" });
                }
                catch (Exception e)
                {
                    return Json(new { error = true, message = "There were problems in saving changes for given entity" });
                }
            }

            return Json(new { error = true, message = "There were problems in saving changes for given entity" });
        }

        // GET: Addresses/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Address address = await db.Addresses.FindAsync(id);
            if (address == null)
            {
                return HttpNotFound();
            }
            return View(address);
        }

        // POST: Addresses/Delete/5
        [HttpPost, ActionName("Delete")]
   //     [ValidateAntiForgeryToken]
        public JsonResult DeleteConfirmed(int id)
        {
            try
            {
                Address address = db.Addresses.Find(id);
                db.Addresses.Remove(address);
                db.SaveChanges();
                return Json(new { success = true, message = "Address "+address.AddressID+" Deleted Successfully" });
            }
            catch (Exception e)
            {
                return Json(new { error = true, message = "There were errors in deleting the specified address" });
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
