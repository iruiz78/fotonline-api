namespace ApiFoto.Repository.Generic
{
    public interface IGenericRepository<T>
    {
        Task<T> Get(int id);
        Task<IEnumerable<T>> GetAll();
        Task<int> SaveRange(IEnumerable<T> list);
        Task Update(T t);
        Task Insert(T t);
        Task Delete(int id);
    }
}