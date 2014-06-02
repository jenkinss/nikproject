using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web.Mvc;
using SoyalWorkTimeWebManager.DAL;
using SoyalWorkTimeWebManager.Models;
using SoyalWorkTimeWebManager.ViewModels;
using WorkTimeModel;

namespace SoyalWorkTimeWebManager.Controllers.BaseControllers
{

    public class BaseLocationController<TContext> : Controller where TContext : WorkTimeManagerContext
    {
        protected EFUnitOfWork<TContext> _worker;

        public virtual EFUnitOfWork<TContext> Worker
        {
            get
            {
                if (_worker == null)
                    _worker = new EFUnitOfWork<TContext>();
                return _worker;
            }
        }

        
         //GET: /Location/

        public ActionResult Index()
        {
            var locations = Worker.LocationRepository.Get(includeProperties: "Sites");
            return View(locations.ToList());
        }

        //
        // GET: /Location/Details/5

        public ActionResult Details(int id = 0)
        {
            FacilityLocation facilitylocation = Worker.LocationRepository.GetByID(id);
            if (facilitylocation == null)
            {
                return HttpNotFound();
            }
            return View(facilitylocation);
        }

        //
        // GET: /Location/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Location/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FacilityLocation facilitylocation)
        {
            if (ModelState.IsValid)
            {
                Worker.LocationRepository.Insert(facilitylocation);
                Worker.Save();
                return RedirectToAction("Index");
            }

            return View(facilitylocation);
        }

        //
        // GET: /Location/Edit/5

        public ActionResult Edit(int id = 0)
        {
            FacilityLocation facilitylocation = Worker.LocationRepository.GetByID(id, "Sites");
            if (facilitylocation == null)
            {
                return HttpNotFound();
            }
            SitesLoadtoViewbag(facilitylocation);
            return View(facilitylocation);
        }

        private void SitesLoadtoViewbag(FacilityLocation location)
        {
            var locationSites = new HashSet<int>(location.Sites.Select(i => i.ID));
            var sites = Worker.SiteRepository.GetAll().Select(item => new AssignedSites
            {
                SiteID = item.ID,
                SiteName = item.Name,
                Assigned = locationSites.Contains(item.ID)
            }).ToList();
            ViewBag.Sites = sites;
        }

        //
        // POST: /Location/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, FormCollection formcollection)
        {
            var location = Worker.LocationRepository.GetByID(id, "Sites");
            //if (TryUpdateModel(location,
            //    "",
            //    new string[] {"Name", "Address", "Sites", "People", "Groups"}))
            //{
            //    //if(String.IsNullOrWhiteSpace())
            //}
            string[] currentSelectedSites = formcollection.GetValues("selectedSites");
            location.Sites.Clear();
            if (currentSelectedSites != null)
            {
                foreach (var site in Worker.SiteRepository.GetAll())
                {
                    if (currentSelectedSites.Contains(site.Name))
                    {
                        location.Sites.Add(site);
                    }
                }
            }
            Worker.LocationRepository.Update(location);
            Worker.Save();
            SitesLoadtoViewbag(location);
            return View(location);
        }

        //
        // GET: /Location/Delete/5

        public ActionResult Delete(int id = 0)
        {
            FacilityLocation facilitylocation = Worker.LocationRepository.GetByID(id);
            if (facilitylocation == null)
            {
                return HttpNotFound();
            }
            return View(facilitylocation);
        }

        //
        // POST: /Location/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                FacilityLocation facilitylocation = Worker.LocationRepository.GetByID(id);
                Worker.LocationRepository.Delete(facilitylocation);
                Worker.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                return RedirectToAction("Delete",
                    new { concurrencyError = true });
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            Worker.Dispose();
            base.Dispose(disposing);
        }
    }
}