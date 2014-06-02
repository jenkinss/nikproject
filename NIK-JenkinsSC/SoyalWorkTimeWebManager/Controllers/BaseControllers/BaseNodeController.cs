using System.Web.Mvc;
using SoyalWorkTimeWebManager.DAL;
using SoyalWorkTimeWebManager.Models;
using WorkTimeModel.APIModels;

namespace SoyalWorkTimeWebManager.Controllers.BaseControllers
{
    
    public class BaseNodeController<TContext> : Controller where TContext : WorkTimeManagerContext
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
        // GET: /Node/

        public ActionResult Index()
        {
            return View(Worker.NodeRepository.GetAll());
        }

        //
        // GET: /Node/Details/5

        public ActionResult Details(int id = 0)
        {
            Node node = Worker.NodeRepository.GetByID(id);
            if (node == null)
            {
                return HttpNotFound();
            }
            return View(node);
        }

        //
        // GET: /Node/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Node/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Node node)
        {
            if (ModelState.IsValid)
            {
                Worker.NodeRepository.Insert(node);
                Worker.Save();
                return RedirectToAction("Index");
            }

            return View(node);
        }

        //
        // GET: /Node/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Node node = Worker.NodeRepository.GetByID(id);
            if (node == null)
            {
                return HttpNotFound();
            }
            return View(node);
        }

        //
        // POST: /Node/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Node node)
        {
            if (ModelState.IsValid)
            {
                Worker.NodeRepository.Update(node);
                Worker.Save();
                return RedirectToAction("Index");
            }
            return View(node);
        }

        //
        // GET: /Node/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Node node = Worker.NodeRepository.GetByID(id);
            if (node == null)
            {
                return HttpNotFound();
            }
            return View(node);
        }

        //
        // POST: /Node/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Node node = Worker.NodeRepository.GetByID(id);
            Worker.NodeRepository.Delete(node);
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