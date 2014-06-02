using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SoyalWorkTimeWebManager.DAL;
using SoyalWorkTimeWebManager.Models;
using WorkTimeModel;

namespace SoyalWorkTimeWebManager.Controllers.BaseControllers
{
    public class BaseCardController<TContext> : Controller where TContext : WorkTimeManagerContext
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
        // GET: /Card/
        [Authorize(Roles = "CardMember")]
        public ActionResult Index(FormCollection formCollection)
        {
            IEnumerable<WTCard> cards = null;
            string[] cardCode = formCollection.GetValues("cardCode");
            string[] userAddress = formCollection.GetValues("userAddress");
            string[] cardID = formCollection.GetValues("cardID");

            if (cardCode != null && cardCode[0] != "")
            {
                string code = cardCode[0];

                if (code.Contains(":"))
                {
                    string[] codes = code.Split(':');
                    int site = int.Parse(codes[0]);
                    int card = int.Parse(codes[1]);
                    cards = Worker.CardRepository.Get(_ => _.SiteCode == site && _.Code == card);
                }
                else
                {
                    int intcode = int.Parse(code);
                    cards = Worker.CardRepository.Get(_ => _.SiteCode == intcode || _.Code == intcode);
                }
            }
            else if (userAddress != null && userAddress[0] != "")
            {
                int number = int.Parse(userAddress[0]);
                cards = Worker.CardRepository.Get(_ => _.UserAddress == number);
            }
            else if (cardID != null && cardID[0] != "")
            {
                int number = int.Parse(cardID[0]);
                cards = Worker.CardRepository.Get(_ => _.ID == number);
            }

            if (cards == null)
            {
                cards = Worker.CardRepository.GetAll();
            }
            return View(cards.ToList());
        }

        //
        // GET: /Card/Details/5


        //TODO: ez nem működik
        private void InitAccesMode()
        {

            var items = new List<SelectListItem>
                {
                    new SelectListItem {Text = "Invalid", Value = "0"},
                    new SelectListItem {Text = "Read Only", Value = "1", Selected = true},
                    new SelectListItem {Text = "Card or Pin", Value = "2"},
                    new SelectListItem {Text = "Card and Pin", Value = "3"},
                    
                };

            ViewBag.AccessMode = items;
        }
         [Authorize(Roles = "CardMember")]
        public ActionResult Details(int id = 0)
        {
            WTCard wtcard = Worker.CardRepository.GetByID(id);
            if (wtcard == null)
            {
                return HttpNotFound();
            }
            return View(wtcard);
        }

        //
        // GET: /Card/Create
         [Authorize(Roles = "CardEditor")]
        public ActionResult Create()
        {
            InitAccesMode();
            return View();
        }

        //
        // POST: /Card/Create
        [Authorize(Roles = "CardEditor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(WTCard wtcard)
        {
            if (ModelState.IsValid)
            {
                Worker.CardRepository.Insert(wtcard);
                Worker.Save();
                return RedirectToAction("Index");
            }
            InitAccesMode();
            return View(wtcard);
        }

        //
        // GET: /Card/Edit/5
        [Authorize(Roles = "CardEditor")]
        public ActionResult Edit(int id = 0)
        {
            WTCard wtcard = Worker.CardRepository.GetByID(id);
            if (wtcard == null)
            {
                return HttpNotFound();
            }
            InitAccesMode();
            return View(wtcard);
        }

        //
        // POST: /Card/Edit/5
        [Authorize(Roles = "CardEditor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(WTCard wtcard)
        {
            if (ModelState.IsValid)
            {
                Worker.CardRepository.Update(wtcard);
                Worker.Save();
                return RedirectToAction("Index");
            }
            InitAccesMode();
            return View(wtcard);
        }

        //
        // GET: /Card/Delete/5
        [Authorize(Roles = "CardEditor")]
        public ActionResult Delete(int id = 0)
        {
            WTCard wtcard = Worker.CardRepository.GetByID(id);
            if (wtcard == null)
            {
                return HttpNotFound();
            }
            return View(wtcard);
        }

        //
        // POST: /Card/Delete/5
        [Authorize(Roles = "CardEditor")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WTCard wtcard = Worker.CardRepository.GetByID(id);
            Worker.CardRepository.Delete(wtcard);
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