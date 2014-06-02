using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using SoyalWorkTimeWebManager.Models;
using WebMatrix.WebData;
using WorkTimeModel;

namespace SoyalWorkTimeWebManager.DAL
{
    public class EFRepository<TEntity> : IRepository<TEntity> where TEntity:PojoBase
    {
        private WorkTimeManagerContext context;
        private DbSet<TEntity> dbSet;
        private UserProfile currentUser;
        private bool isRoot;
        
        public EFRepository(WorkTimeManagerContext context)
        {
            this.context = context;
            dbSet = context.Set<TEntity>();
            getUserId();
        }

        public bool IsRoot
        {
            get { return isRoot; }
        }

        public UserProfile CurrentUser
        {
            get { return currentUser; }
        }

        private void getUserId()
        {
            try
            {
                var LoggedInUserId = WebSecurity.GetUserId(System.Web.HttpContext.Current.User.Identity.Name);
                var udb = new UsersContext();
                var user = udb.UserProfiles.FirstOrDefault(_ => _.UserId == LoggedInUserId);
                if (user != null && user.LocationID == -1)
                    isRoot = true;
                else
                    currentUser = user;
            }
            catch (Exception)
            {
                isRoot = true;
            }
        }

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> where)
        {
            //my extension 2014.04.09
            var result = dbSet.Where(entity => !entity.IsDeleted);
            return result.FirstOrDefault(where);
        }

        public IEnumerable<TEntity> GetAll()
        {
            //my extension 2014.04.09
            var result = dbSet.Where(entity => !entity.IsDeleted);
            return result.ToList();
        }

        public void Insert(TEntity entity)
        {
            if (typeof (TEntity) == typeof (WorkTimeEvent))
            {
                var wevent = entity as WorkTimeEvent;
                if (wevent != null)
                {
                    var levent = new LoggedEvent
                    {
                        Direction = wevent.Direction,
                        Person = wevent.Person,
                        Site = wevent.Site,
                        SiteID = wevent.SiteID,
                        EventType = wevent.EventType,
                        PersonID = wevent.PersonID,
                        TimeStamp = wevent.TimeStamp   
                    };
                    context.LoggedEvents.Add(levent);
                }
                
            }
            dbSet.Add(entity);
        }

        public void InsertEvent(TEntity entity, bool insertToLog, bool instertToWork)
        {
            if (typeof(TEntity) == typeof(WorkTimeEvent) && insertToLog)
            {
                var wevent = entity as WorkTimeEvent;
                if (wevent != null)
                {
                    var levent = new LoggedEvent
                    {
                        Direction = wevent.Direction,
                        Person = wevent.Person,
                        Site = wevent.Site,
                        SiteID = wevent.SiteID,
                        EventType = wevent.EventType,
                        PersonID = wevent.PersonID,
                        TimeStamp = wevent.TimeStamp
                    };
                    context.LoggedEvents.Add(levent);
                }

            }
            if(instertToWork)
                dbSet.Add(entity);
        }

        public void Delete(TEntity entity)
        {
            entity.IsDeleted = true;
            context.Entry(entity).State =
                EntityState.Modified;
        }

        public void Update(TEntity entity)
        {
            context.Entry(entity).State =
                EntityState.Modified;
        }

        public TEntity GetByID(int id)
        {
            //my extension 2014.04.09
            var result=dbSet.Find(id);
            if (result.IsDeleted)
                result = dbSet.Where(entity => !entity.IsDeleted).FirstOrDefault(entity => entity.ID == id);
            return result;
        }

        public TEntity GetByID(int id, params string[] children)
        {
            if (children == null || children.Length == 0)
            {
                return dbSet.SingleOrDefault(e => e.ID == id);
            }
            DbQuery<TEntity> query = children.Aggregate<string, DbQuery<TEntity>>(dbSet, (current, child) => current.Include(child));
            return query.SingleOrDefault(e => e.ID == id && !e.IsDeleted);
        }

        public void Delete(int id)
        {
            TEntity entity = GetByID(id);
            Delete(entity);
        }

        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> @orderby = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet.Where(entity => !entity.IsDeleted);
            if (filter != null)
                query = query.Where(filter);
            foreach (var include in
                includeProperties.Split(
                new[] { ',' },
                StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(include);
            }
            if (orderby != null)
                return orderby(query).ToList();
            return query.ToList();
        }
    }
}