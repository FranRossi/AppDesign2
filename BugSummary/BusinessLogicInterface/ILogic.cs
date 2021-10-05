namespace BusinessLogicInterface
{
    public interface ILogic<T>
    {
        public void Add(T entity);
        T Get(string token);
    }
}