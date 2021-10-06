using System.Collections.Generic;
using DataAccessInterface;

namespace DataAccess
{
    public abstract class BaseRepository<T> : IRepository<T> where T : class
    {
        protected BugSummaryContext Context { get; set; }

        public virtual void Add(T entity)
        {
            var entities = Context.Set<T>();
            entities.Add(entity);
        }

        public void Save()
        {
            Context.SaveChanges();
        }
    }
}