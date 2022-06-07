using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
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

        public async Task<T> GetEntityWithSpec(ISpecification<T> spec)
        {
            T entity = await ApplySpecification(spec).FirstOrDefaultAsync();
            return entity;
        }

        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
        {
            List<T> entities = await ApplySpecification(spec).ToListAsync();
            return entities;
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            var query = SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), spec);
            return query;
        }
    }
}