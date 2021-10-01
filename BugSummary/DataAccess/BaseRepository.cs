using DataAccessInterface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public abstract class BaseRepository<T>: IRepository<T> where T : class
    {
        protected BugSummaryContext Context { get; set; }


        public void Add(T entity)
        {
            Context.Set<T>().Add(entity);
        }

        public abstract IEnumerable<T> GetAll();

        public void Save()
        {
            Context.SaveChanges();
        }
    }
}
