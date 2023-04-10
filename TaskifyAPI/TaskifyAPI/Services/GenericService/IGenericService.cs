namespace TaskifyAPI.Services.GenericService
{
    public interface IGenericService<T>
    {
        Task<List<T>> GetAll();

        Task<T?> GetById(int id);

        Task Create(T entity);

        Task Update(T entity);

        Task Delete(T entity);
    }
}
