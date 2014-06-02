using System;
using System.Collections.Generic;
using System.Web.Mvc;
using SoyalWorkTimeWebManager.DAL;
using SoyalWorkTimeWebManager.Models;
using SoyalWorkTimeWebManager.Models.Helpers;
using SoyalWorkTimeWebManager.ViewModels;

namespace SoyalWorkTimeWebManager.Controllers.BaseControllers
{
    public class BaseTableController<TContext> : Controller where TContext : WorkTimeManagerContext
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
        // GET: /BaseTable/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Riport()
        {
            // formCollection
            

            var persons = Worker.PersonRepository.GetAll();
            var riports = new List<WorkTimeRiport>();

            

            foreach (var person in persons)
            {
                var s = new DateTime(2014, 04, 26);
                var e = DateTime.Today;
                var hourOfDays = new List<HourOfDay>();
                while (s<e)
                {
                    hourOfDays.Add(WorkTimeCounter.Compute(person, s, Worker));
                    s = s.AddDays(1);
                }


                var rip = new WorkTimeRiport(person, hourOfDays);
                riports.Add(rip);
            }

            var model = new RiportViewModel();
            model.WorkTimeRiports = riports;

            return View(model);
        }

    }
}
