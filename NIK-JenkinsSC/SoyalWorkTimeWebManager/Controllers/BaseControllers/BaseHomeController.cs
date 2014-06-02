using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SoyalAR721API;
using SoyalBISAPI;
using SoyalBISAPI.Common;
using SoyalWorkTimeWebManager.DAL;
using SoyalWorkTimeWebManager.Models;
using SoyalWorkTimeWebManager.ViewModels;
using WorkTimeModel;

namespace SoyalWorkTimeWebManager.Controllers.BaseControllers
{
    public class BaseHomeController<TContext> : Controller where TContext : WorkTimeManagerContext
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

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MonthlyReport()
        {
            ViewBag.PersonList = new SelectList(Worker.PersonRepository.GetAll(), "ID", "Name");
            return View();
        }

        [HttpPost]
        public FileContentResult ExportCSV(FormCollection formCollection)
        {
            string csv = "";
            var data = formCollection.GetValues("csvdata");
            if (data != null) csv += data[0];
            return File(new System.Text.UTF8Encoding().GetBytes(csv), "text/csv", DateTime.Today + "Report.csv");
        }

        //http://stackoverflow.com/questions/815234/c-sharp-foreach-loop-hashtable-issue
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MonthlyReport(string id, FormCollection formCollection)
        {
            int nid = int.Parse(id);
            var person = Worker.PersonRepository.GetByID(nid);
            var events = Worker.WorkTimeRepository.Get(_ => _.PersonID == nid);
            var sDate = formCollection.GetValues("reportStartDate");
            var eDate = formCollection.GetValues("reportEndDate");
            var dateHash = new Dictionary<string, List<string>>();
            if (sDate != null && eDate != null)
            {
                DateTime startDate = Convert.ToDateTime(sDate[0]);
                DateTime endDate = Convert.ToDateTime(eDate[0]);
                while (startDate < endDate)
                {
                    string d = startDate.ToShortDateString();
                    if (events != null)
                    {
                        var daylyList = (from @event in events where @event.TimeStamp.ToString().Contains(d) select @event.TimeStamp.ToShortTimeString() + "-" + @event.Direction).ToList();
                        dateHash.Add(d, daylyList);
                    }
                    startDate = startDate.AddDays(1);
                }
            }
            var report = new MoveReportViewModel { MoveDictionary = dateHash, Birth = person.Birth.ToShortDateString(), Name = person.Name };
            ViewBag.ReportData = report;
            ViewBag.PersonList = new SelectList(Worker.PersonRepository.GetAll(), "ID", "Name");
            return View(report);
        }

        }
}
