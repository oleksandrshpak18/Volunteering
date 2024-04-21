namespace Volunteering.Data.Interfaces
{
    public interface IDomainService<T1, T2> 
        where T1 : class // ViewModel Class
        where T2 : class // Model Class
    {
        public T1 ConvertToVm(T2 obj);
        public IEnumerable<T2> GetAll();
        public T2 Add(T1 obj);
    }
}
