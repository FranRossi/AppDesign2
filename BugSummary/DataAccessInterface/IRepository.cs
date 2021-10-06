using System.Collections.Generic;

namespace DataAccessInterface
{
    public interface IRepository<T>
    {
        void Add(T entity);

        IEnumerable<T> GetAllFiltered();

        void Save();
    }
}