using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using SoyalWorkTimeWebManager.Models;

namespace SoyalWorkTimeWebManager.DAL
{
    interface IRepository<TEntity>
    {
        IEnumerable<TEntity> GetAll();
        void Insert(TEntity entity);
        void Delete(TEntity entity);
        void Update(TEntity entity);
        TEntity GetByID(int id);
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> where);
        UserProfile CurrentUser { get; }
        bool IsRoot { get; }
        TEntity GetByID(int id, params string[] children);
        void Delete(int id);
        IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>
            orderby = null,
            string includeProperties = "");
    }
}
