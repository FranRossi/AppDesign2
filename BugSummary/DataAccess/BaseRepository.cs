using System.Collections.Generic;
using DataAccessInterface;

namespace DataAccess
{
    public abstract class BaseRepository<T>
    {
        protected BugSummaryContext Context { get; set; }

        public void Save()
        {
            Context.SaveChanges();
        }
    }
}