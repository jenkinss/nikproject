using System.Web.Mvc;
using SoyalWorkTimeWebManager.DAL;
using SoyalWorkTimeWebManager.Models;
using WorkTimeModel;

namespace SoyalWorkTimeWebManager.Controllers.BaseControllers
{

    public class BaseSiteController<TContext> : Controller where TContext : WorkTimeManagerContext
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

        //
        // GET: /Site/

        public ActionResult Index()
        {
            var sites = Worker.SiteRepository.Get(includeProperties: "Node");
            return View(sites);
        }

        //
        // GET: /Site/Details/5

        public ActionResult Details(int id = 0)
        {
            Site site = Worker.SiteRepository.GetByID(id);
            if (site == null)
            {
                return HttpNotFound();
            }
            return View(site);
        }

        //
        // GET: /Site/Create

        public ActionResult Create()
        {
            ViewBag.NodeID = new SelectList(Worker.NodeRepository.GetAll(), "ID", "Description");
            return View();
        }

        //
        // POST: /Site/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Site site)
        {
            if (ModelState.IsValid)
            {
                Worker.SiteRepository.Insert(site);
                Worker.Save();
                return RedirectToAction("Index");
            }

            ViewBag.NodeID = new SelectList(Worker.NodeRepository.GetAll(), "ID", "Description", site.NodeID);
            return View(site);
        }

        //
        // GET: /Site/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Site site = Worker.SiteRepository.GetByID(id);
            if (site == null)
            {
                return HttpNotFound();
            }
            ViewBag.NodeID = new SelectList(Worker.NodeRepository.GetAll(), "ID", "Description", site.NodeID);
            return View(site);
        }

        //
        // POST: /Site/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Site site)
        {
            if (ModelState.IsValid)
            {
                Worker.SiteRepository.Update(site);
                Worker.Save();
                return RedirectToAction("Index");
            }
            ViewBag.NodeID = new SelectList(Worker.NodeRepository.GetAll(), "ID", "Description", site.NodeID);
            return View(site);
        }

        //
        // GET: /Site/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Site site = Worker.SiteRepository.GetByID(id);
            if (site == null)
            {
                return HttpNotFound();
            }
            return View(site);
        }

        //
        // POST: /Site/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Site site = Worker.SiteRepository.GetByID(id);
            Worker.SiteRepository.Delete(site);
            Worker.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            Worker.Dispose();
            base.Dispose(disposing);
        }
    }
}