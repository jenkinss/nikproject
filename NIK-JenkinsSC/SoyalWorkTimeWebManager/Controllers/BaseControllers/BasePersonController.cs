using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SoyalBISAPI;
using SoyalBISAPI.Common;
using SoyalBISAPI.HexCommands;
using SoyalWorkTimeWebManager.DAL;
using SoyalWorkTimeWebManager.Models;
using SoyalWorkTimeWebManager.ViewModels.Assignments;
using SoyalWorkTimeWebManager.ViewModels.Helpers;
using WorkTimeModel;
using WorkTimeModel.APIModels;

namespace SoyalWorkTimeWebManager.Controllers.BaseControllers
{

    public class BasePersonController<TContext> : Controller where TContext : WorkTimeManagerContext
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
        // GET: /Person/
        [Authorize(Roles = "PersonMember")]
        public ActionResult Index(FormCollection formCollection)
        {
            IEnumerable<Person> persons = null;
            string[] personName = formCollection.GetValues("personName");
            string[] cardBoardNumber = formCollection.GetValues("cardBoardNumber");
            string[] professionalClass = formCollection.GetValues("professionalClass");
            string[] cardCode = formCollection.GetValues("cardCode");
            string[] group = formCollection.GetValues("group");
            if (personName != null && personName[0] != "")
            {
                string name = personName[0];
                persons = Worker.PersonRepository.Get(_ => _.Name.Contains(name) || _.NameUa.Contains(name),
                    includeProperties: "Card, PersonGroups");
            }
            else if (cardBoardNumber != null && cardBoardNumber[0] != "")
            {
                int number = int.Parse(cardBoardNumber[0]);
                persons = Worker.PersonRepository.Get(_ => _.CardBoardNumber == number,
                    includeProperties: "Card, PersonGroups");
            }
            else if (professionalClass != null && professionalClass[0] != "")
            {
                string profession = professionalClass[0];
                persons = Worker.PersonRepository.Get(_ => _.ProfessionalClass.Contains(profession) || _.Post.Contains(profession) || _.SubUnit.Contains(profession),
                    includeProperties: "Card, PersonGroups");
            }
            else if (cardCode != null && cardCode[0] != "")
            {
                string code = cardCode[0];
                if (code.Contains(":"))
                {
                    string[] codes = code.Split(':');
                    int site = int.Parse(codes[0]);
                    int card = int.Parse(codes[1]);
                    persons = Worker.PersonRepository.Get(_ => _.Card.SiteCode == site && _.Card.Code == card,
                        includeProperties: "Card, PersonGroups");
                }
                else
                {
                    int intcode = int.Parse(code);
                    persons = Worker.PersonRepository.Get(_ => _.Card.SiteCode == intcode || _.Card.Code == intcode,
                        includeProperties: "Card, PersonGroups");
                }
                
            }
            else if (group != null && group[0] != "")
            {
                string strgroup = group[0];
                persons = Worker.PersonRepository.Get(_ => _.PersonGroups.Any(g => g.Name==strgroup),
                    includeProperties: "Card, PersonGroups");
            }
            if (persons == null)
            {
                persons = Worker.PersonRepository.Get(includeProperties: "Card, PersonGroups");
            }

            

            return View(persons.ToList());
        }

        //
        // GET: /Person/Details/5
        [Authorize(Roles = "PersonMember")]
        public ActionResult Details(int id = 0)
        {
            Person person = Worker.PersonRepository.GetByID(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }
        [Authorize(Roles = "PersonSendToReaders")]
        public ActionResult SendToReader(int id = 0)
        {
            Person person = Worker.PersonRepository.GetByID(id, "Location");
            if (person == null)
            {
                return HttpNotFound();
            }
            var groups = person.PersonGroups;
            var locations = groups.Select(gr => gr.Location).ToList();
            var nodes = new List<Node>();
            foreach (FacilityLocation location in locations)
                foreach (Site site in location.Sites)
                {
                    int? siteId = site.NodeID;
                    Node currentNode = Worker.NodeRepository.FirstOrDefault(node => siteId != null && node.ID == siteId);
                    nodes.Add(currentNode);
                }
            foreach (var node in nodes)
            {
                try
                {
                    using (ISoyalCommandClient soyalcommander = Commander.Get(Series.V721Hv3, node.IP, node.Port, node.Number))
                    {
                        AccessMode mode = AccessConverter.GetMode(person.Card.Mode);
                        soyalcommander.AddCardContent(node.Number,
                            person.Card.Code,
                            person.Card.UserAddress,
                            person.Card.SiteCode,
                            person.Card.PinCode,
                            mode,
                            person.Card.AntiPassBack,
                            person.Card.TimeZone);
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.ReaderMessage = ex.Message;
                }
            }


            return RedirectToAction("Index");
        }

        //
        // GET: /Person/Create
         [Authorize(Roles = "PersonEditor")]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Person/Create
        [Authorize(Roles = "PersonEditor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Person person)
        {
            if (ModelState.IsValid)
            {
                Worker.PersonRepository.Insert(person);
                Worker.Save();

                return RedirectToAction("Index");
            }


            return View(person);
        }

        //
        // GET: /Person/Edit/5
        [Authorize(Roles = "PersonEditor")]
        public ActionResult Edit(int id = 0)
        {
            Person person = Worker.PersonRepository.GetByID(id, "Card", "PersonGroups");
            if (person == null)
            {
                return HttpNotFound();
            }
            GroupsLoadToVeiwbag(person);
            return View(person);
        }

        //
        // POST: /Person/Edit/5
        [Authorize(Roles = "PersonEditor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, FormCollection formCollection)
        {
            var person = Worker.PersonRepository.GetByID(id, "Card", "PersonGroups");

            if (TryUpdateModel(person, "",
                    new[] {"NameUa", "Name",
                        "CardBoardNumber", "Birth",
                        "ProfessionalClass", "Post", "SubUnit", "WorkStyle"}))
            {
                if (String.IsNullOrWhiteSpace(person.WorkStyle))
                {
                    person.WorkStyle = null;
                }
            }

            string[] currentGrups = formCollection.GetValues("selectedGroups");
            string[] sitecode = formCollection.GetValues("Card.SiteCode");
            string[] cardcode = formCollection.GetValues("Card.Code");

            person.PersonGroups.Clear();
            if (currentGrups != null)
            {
                foreach (var personGroup in Worker.GroupRepository.GetAll())
                {
                    if (currentGrups.Contains(personGroup.Name))
                    {
                        person.PersonGroups.Add(personGroup);
                    }
                }
            }
            Worker.PersonRepository.Update(person);
            Worker.Save();
            GroupsLoadToVeiwbag(person);
            if (ModelState.IsValid)
            {
                if (cardcode != null && (sitecode != null && (sitecode[0] != "" || cardcode[0] != "")))
                {
                    int cc = int.Parse(cardcode[0]);
                    int sc = int.Parse(sitecode[0]);
                    var card = Worker.CardRepository.FirstOrDefault(c => c.Code == cc && c.SiteCode == sc);
                    if (card != null)
                    {
                        person.CardID = 0;
                        card.UserAddress = person.ID;
                        person.Card = card;
                        person.CardID = card.ID;
                        Worker.PersonRepository.Update(person);
                        Worker.CardRepository.Update(card);
                        Worker.Save();
                    }
                    else
                    {
                        person.Card = null;
                        person.CardID = null;
                    }
                }
                else
                {
                    person.Card = null;

                }
                Worker.PersonRepository.Update(person);
                Worker.Save();
                return RedirectToAction("Index");
            }
            return View(person);
        }

        private void GroupsLoadToVeiwbag(Person person)
        {
            var personGroups = new HashSet<int>(person.PersonGroups.Select(g => g.ID));
            List<AssignedGroups> groups = (from @group in Worker.GroupRepository.GetAll()
                                           select new AssignedGroups
                                           {
                                               GroupID = @group.ID,
                                               GroupName = @group.Name,
                                               Assigned = personGroups.Contains(@group.ID)
                                           }).ToList();
            ViewBag.Groups = groups;
        }

        //
        // GET: /Person/Delete/5
        [Authorize(Roles = "PersonEditor")]
        public ActionResult Delete(int id = 0)
        {
            Person person = Worker.PersonRepository.GetByID(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        //
        // POST: /Person/Delete/5
        [Authorize(Roles = "PersonEditor")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Person person = Worker.PersonRepository.GetByID(id);
            Worker.PersonRepository.Delete(person);
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