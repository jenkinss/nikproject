using System;
using System.Linq;
using System.Web.Mvc;
using SoyalWorkTimeWebManager.DAL;
using SoyalWorkTimeWebManager.Models;
using SoyalWorkTimeWebManager.Models.Helpers;
using WorkTimeModel;

namespace SoyalWorkTimeWebManager.Controllers.BaseControllers
{
    
    public class BaseGroupController<TContext> : Controller where TContext : WorkTimeManagerContext
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
        // GET: /PersonGroup/
        [Authorize(Roles = "GroupMember")]
        public ActionResult Index()
        {
            return View(Worker.GroupRepository.GetAll().ToList());
        }

        //
        // GET: /PersonGroup/Details/5
        [Authorize(Roles = "GroupMember")]
        public ActionResult Details(int id = 0)
        {
            PersonGroup persongroup = Worker.GroupRepository.GetByID(id);
            if (persongroup == null)
            {
                return HttpNotFound();
            }
            return View(persongroup);
        }

        //
        // GET: /PersonGroup/Create
        [Authorize(Roles = "GroupEditor")]
        public ActionResult Create()
        {
            ViewBag.LocationID = new SelectList(Worker.LocationRepository.GetAll().ToList(), "ID", "Name");
            return View();
        }

        //
        // POST: /PersonGroup/Create
        [Authorize(Roles = "GroupEditor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PersonGroup persongroup)
        {
            if (ModelState.IsValid)
            {
                Worker.GroupRepository.Insert(persongroup);
                Worker.Save();
                return RedirectToAction("Index");
            }
            ViewBag.LocationID = new SelectList(Worker.LocationRepository.GetAll().ToList(), "ID", "Name", persongroup.LocationID);
            return View(persongroup);
        }

        //
        // GET: /PersonGroup/Edit/5
        [Authorize(Roles = "GroupEditor")]
        public ActionResult Edit(int id = 0)
        {
            PersonGroup persongroup = Worker.GroupRepository.GetByID(id);
            if (persongroup == null)
            {
                return HttpNotFound();
            }
            ViewBag.LocationID = new SelectList(Worker.LocationRepository.GetAll().ToList(), "ID", "Name", persongroup.LocationID);
            return View(persongroup);
        }



        //
        // POST: /PersonGroup/Edit/5
        [Authorize(Roles = "GroupEditor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PersonGroup persongroup)
        {
            if (ModelState.IsValid)
            {
                Worker.GroupRepository.Update(persongroup);
                Worker.Save();
                return RedirectToAction("Index");
            }
            ViewBag.LocationID = new SelectList(Worker.LocationRepository.GetAll().ToList(), "ID", "Name", persongroup.LocationID);
            return View(persongroup);
        }

        //
        // GET: /PersonGroup/Delete/5
        [Authorize(Roles = "GroupEditor")]
        public ActionResult Delete(int id = 0)
        {
            PersonGroup persongroup = Worker.GroupRepository.GetByID(id);
            if (persongroup == null)
            {
                return HttpNotFound();
            }
            return View(persongroup);
        }

        //
        // POST: /PersonGroup/Delete/5
        [Authorize(Roles = "GroupEditor")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PersonGroup persongroup = Worker.GroupRepository.GetByID(id);
            Worker.GroupRepository.Delete(persongroup);
            Worker.Save();
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "GroupSendToReaders")]
        public ActionResult SendToReader(int id = 0)
        {
            try
            {
                CardSender.DownloadCards(Worker, id);
            }
            catch (Exception ex)
            {
                return HttpNotFound(ex.Message);
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