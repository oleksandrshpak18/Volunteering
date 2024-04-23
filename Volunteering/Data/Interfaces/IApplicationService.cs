namespace Volunteering.Data.Interfaces
{
    public interface IApplicationService<T>
        where T : class // viewmodel class
    {
        public IEnumerable<T> GetAll();
        public T Add(T obj);
    }
}
