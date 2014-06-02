using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using DotNetOpenAuth.OpenId.Extensions.AttributeExchange;
using SoyalWorkTimeWebManager.Models;
using WorkTimeModel;
using WorkTimeModel.APIModels;

namespace SoyalWorkTimeWebManager.DAL
{
    public class EFUnitOfWork<TLocation>:IDisposable where TLocation:WorkTimeManagerContext
    {
        TLocation context = (TLocation)Activator.CreateInstance(typeof(TLocation));
        private EFRepository<Person> personRepository;
        

        public EFRepository<Person> PersonRepository
        {
            get { return personRepository ?? (personRepository = new EFRepository<Person>(context)); }
        }

        private EFRepository<WTCard> cardRepository;

        public EFRepository<WTCard> CardRepository
        {
            get { return cardRepository ?? (cardRepository = new EFRepository<WTCard>(context)); }
        }

        private EFRepository<Node> nodeRepository;

        public EFRepository<Node> NodeRepository
        {
            get { return nodeRepository ?? (nodeRepository = new EFRepository<Node>(context)); }
        }

        private EFRepository<FacilityLocation> locationRepository;

        public EFRepository<FacilityLocation> LocationRepository
        {
            get { return locationRepository ?? (locationRepository = new EFRepository<FacilityLocation>(context)); }
        }

        private EFRepository<PersonGroup> groupRepository;

        public EFRepository<PersonGroup> GroupRepository
        {
            get { return groupRepository ?? (groupRepository = new EFRepository<PersonGroup>(context)); }
        }

        private EFRepository<Site> siteRepository;

        public EFRepository<Site> SiteRepository
        {
            get { return siteRepository ?? (siteRepository = new EFRepository<Site>(context)); }
        }

        private EFRepository<WorkTimeEvent> worktimeRepository;

        public EFRepository<WorkTimeEvent> WorkTimeRepository
        {
            get { return worktimeRepository ?? (worktimeRepository = new EFRepository<WorkTimeEvent>(context)); }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    // managed resources free
                    if(context!=null)context.Dispose();
                }
                // unmanaged resources free
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}