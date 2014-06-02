using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SoyalWorkTimeWebManager.DAL;
using SoyalWorkTimeWebManager.Models;
using SoyalWorkTimeWebManager.Models.Helpers;
using WorkTimeModel;

namespace SoyalWorkTimeWebManager.Controllers.BaseControllers
{

    public class BaseEventController<TContext> : Controller where TContext : WorkTimeManagerContext
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
        // GET: /Event/
        [Authorize(Roles = "EventMember")]
        public ActionResult Index(FormCollection formCollection)
        {
            //var worktimeevents = Worker.WorkTimeRepository.Get(includeProperties: "Site, Person").OrderByDescending(_ => _.TimeStamp);
            IEnumerable<WorkTimeEvent> workTimeEvents = null;
            string[] personName = formCollection.GetValues("personName");
            string[] selectedDate = formCollection.GetValues("selectedDate");
            string[] selectedDirection = formCollection.GetValues("selectedDirection");
            string[] selectedType = formCollection.GetValues("selectedType");
            string[] intervallSelectedStartDate = formCollection.GetValues("intervallSelectedStartDate");
            string[] intervallSelectedEndDate = formCollection.GetValues("intervallSelectedEndDate");
            string[] intervallPersonName = formCollection.GetValues("intervallPersonName");
            string[] personIntervallSelectedStartDate = formCollection.GetValues("personIntervallSelectedStartDate");
            string[] personIntervallSelectedEndDate = formCollection.GetValues("personIntervallSelectedEndDate");
            if (personName != null && personName[0] != "")
            {
                string name = personName[0];
                workTimeEvents = Worker.WorkTimeRepository.Get(_=>_.Person.Name.Contains(name), includeProperties: "Site, Person").OrderByDescending(_ => _.TimeStamp);
                
            }
            else if (selectedDate != null && selectedDate[0] != "")
            {

                DateTime date = FormDateConverter.Convert(selectedDate[0]);
                DateTime ddate = date.AddDays(1);
                workTimeEvents =
                    Worker.WorkTimeRepository.Get(_ => _.TimeStamp >= date && _.TimeStamp <= ddate,
                        includeProperties: "Site, Person").OrderByDescending(_ => _.TimeStamp);

            }
            else if (selectedDirection != null && selectedDirection[0] != "")
            {

                string direction = selectedDirection[0];
                workTimeEvents = Worker.WorkTimeRepository.Get(_ => _.Direction.Contains(direction), includeProperties: "Site, Person").OrderByDescending(_ => _.TimeStamp);

            }
            else if (selectedType != null && selectedType[0] != "")
            {

                string sType = selectedType[0];
                workTimeEvents = Worker.WorkTimeRepository.Get(_ => _.EventType.Contains(sType), includeProperties: "Site, Person").OrderByDescending(_ => _.TimeStamp);

            }
            else if (intervallSelectedStartDate != null && intervallSelectedStartDate[0] != "" && intervallSelectedEndDate != null && intervallSelectedEndDate[0] != "")
            {

                DateTime start = FormDateConverter.Convert(intervallSelectedStartDate[0]);
                DateTime end = FormDateConverter.Convert(intervallSelectedEndDate[0]);
                if (end > start)
                {
                    workTimeEvents =
                        Worker.WorkTimeRepository.Get(_ => _.TimeStamp >= start && _.TimeStamp <= end,
                            includeProperties: "Site, Person").OrderByDescending(_ => _.TimeStamp);
                }
                

            }
            else if (personIntervallSelectedStartDate != null && personIntervallSelectedStartDate[0] != "" && personIntervallSelectedEndDate != null && personIntervallSelectedEndDate[0] != "" && intervallPersonName != null && intervallPersonName[0]!="")
            {
                string name = intervallPersonName[0];
                DateTime start = FormDateConverter.Convert(personIntervallSelectedStartDate[0]);
                DateTime end = FormDateConverter.Convert(personIntervallSelectedEndDate[0]);
                if (end > start)
                {
                    workTimeEvents =
                        Worker.WorkTimeRepository.Get(
                            _ => _.Person.Name.Contains(name) && _.TimeStamp >= start && _.TimeStamp <= end,
                            includeProperties: "Site, Person").OrderByDescending(_ => _.TimeStamp);
                }


            }
            


            if (workTimeEvents == null)
            {
                workTimeEvents = Worker.WorkTimeRepository.Get(includeProperties: "Site, Person").OrderByDescending(_ => _.TimeStamp);
            }
            return View(workTimeEvents.ToList());
        }

        //
        // GET: /Event/Details/5
        [Authorize(Roles = "EventMember")]
        public ActionResult Details(int id = 0)
        {
            WorkTimeEvent worktimeevent = Worker.WorkTimeRepository.GetByID(id);
            if (worktimeevent == null)
            {
                return HttpNotFound();
            }
            return View(worktimeevent);
        }

        //
        // GET: /Event/Create
        [Authorize(Roles = "EventEditor")]
        public ActionResult Create()
        {
            ViewBag.SiteID = new SelectList(Worker.SiteRepository.GetAll(), "ID", "Description");
            ViewBag.PersonID = new SelectList(Worker.PersonRepository.GetAll(), "ID", "NameUa");
            return View();
        }

        //
        // POST: /Event/Create
        [Authorize(Roles = "EventEditor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(WorkTimeEvent worktimeevent)
        {
            if (ModelState.IsValid)
            {
                Worker.WorkTimeRepository.Insert(worktimeevent);
                Worker.Save();
                return RedirectToAction("Index");
            }

            ViewBag.SiteID = new SelectList(Worker.SiteRepository.GetAll(), "ID", "Description", worktimeevent.SiteID);
            ViewBag.PersonID = new SelectList(Worker.PersonRepository.GetAll(), "ID", "NameUa", worktimeevent.PersonID);
            return View(worktimeevent);
        }

        //
        // GET: /Event/Edit/5
        [Authorize(Roles = "EventEditor")]
        public ActionResult Edit(int id = 0)
        {
            WorkTimeEvent worktimeevent = Worker.WorkTimeRepository.GetByID(id);
            if (worktimeevent == null)
            {
                return HttpNotFound();
            }
            ViewBag.SiteID = new SelectList(Worker.SiteRepository.GetAll(), "ID", "Description", worktimeevent.SiteID);
            ViewBag.PersonID = new SelectList(Worker.PersonRepository.GetAll(), "ID", "NameUa", worktimeevent.PersonID);
            return View(worktimeevent);
        }

        //
        // POST: /Event/Edit/5
        [Authorize(Roles = "EventEditor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(WorkTimeEvent worktimeevent)
        {
            if (ModelState.IsValid)
            {
                Worker.WorkTimeRepository.Update(worktimeevent);
                Worker.Save();
                return RedirectToAction("Index");
            }
            ViewBag.SiteID = new SelectList(Worker.SiteRepository.GetAll(), "ID", "Description", worktimeevent.SiteID);
            ViewBag.PersonID = new SelectList(Worker.PersonRepository.GetAll(), "ID", "NameUa", worktimeevent.PersonID);
            return View(worktimeevent);
        }

        //
        // GET: /Event/Delete/5
        [Authorize(Roles = "EventEditor")]
        public ActionResult Delete(int id = 0)
        {
            WorkTimeEvent worktimeevent = Worker.WorkTimeRepository.GetByID(id);
            if (worktimeevent == null)
            {
                return HttpNotFound();
            }
            return View(worktimeevent);
        }

        //
        // POST: /Event/Delete/5
        [Authorize(Roles = "EventEditor")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WorkTimeEvent worktimeevent = Worker.WorkTimeRepository.GetByID(id);
            Worker.WorkTimeRepository.Delete(worktimeevent);
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