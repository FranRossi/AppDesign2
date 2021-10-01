using System;

namespace BusinessLogicInterface
{
    public interface ILogic<T>
    {
        public void Add(T entity);
    }
}
