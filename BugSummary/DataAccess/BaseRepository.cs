using DataAccessInterface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public abstract class BaseRepository<T> : IRepository<T> where T : class
    {
        protected BugSummaryContext Context { get; set; }

        public virtual void Add(T entity)
        {
            DbSet<T> entities = Context.Set<T>();
            entities.Add(entity);
        }

        public abstract IEnumerable<T> GetAll();

        public void Save()
        {
            Context.SaveChanges();
        }
    }
}
