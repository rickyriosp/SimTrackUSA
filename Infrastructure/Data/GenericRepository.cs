using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext _context;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            T entity = await _context.Set<T>().FindAsync(id);
            return entity;
        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            List<T> entities = await _context.Set<T>().ToListAsync();
            return entities;
        }
    }
}