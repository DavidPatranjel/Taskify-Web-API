using Microsoft.EntityFrameworkCore;
using TaskifyAPI.Data;

namespace TaskifyAPI.Services.GenericService
{
    public class GenericService<T> : IGenericService<T> where T : class
    {
        protected readonly AppDbContext _db;

        public GenericService(AppDbContext db)
        {
            this._db = db;
        }

        public async Task Create(T entity)
        {
            await _db.Set<T>().AddAsync(entity);
        }

        public async Task Delete(T entity)
        {
            _db.Set<T>().Remove(entity);
            await Task.CompletedTask;
        }

        public async Task Update(T entity)
        {
            _db.Set<T>().Update(entity);
            await Task.CompletedTask;
        }

        public async Task<List<T>> GetAll()
        {
            return await _db.Set<T>().ToListAsync();
        }

        public async Task<T?> GetById(int id)
        {
            return await _db.Set<T>().FindAsync(id);
        }
    }
}
