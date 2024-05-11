namespace Volunteering.Data.Interfaces
{
    public interface IDomainService<T1, T2> 
        where T1 : class // ViewModel Class
        where T2 : class // Model Class
    {
        public IEnumerable<T2> GetAll();
        public T2? Get(Guid id);
        public T2 Add(T1 obj);
        public T2 Update(T1 obj);
        public bool Delete(Guid id);
    }
}
