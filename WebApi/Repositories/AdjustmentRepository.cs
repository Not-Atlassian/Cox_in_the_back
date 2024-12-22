using Microsoft.EntityFrameworkCore;
using WebApi.Context;
using WebApi.Models;
using WebApi.RepositoryInterfaces;

namespace WebApi.Repositories
{
    public class AdjustmentRepository : IAdjustmentRepository
    {
        private readonly JyrosContext _context;
        public AdjustmentRepository(JyrosContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Adjustment>> GetAll()
        {
            return await _context.Adjustments.ToListAsync();
        }

        public async Task<Adjustment> Add(Adjustment entity)
        {
            try
            {
                entity.Id = default;
            }
            catch (System.Exception)
            {
                throw;
            }
            _context.Adjustments.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Adjustment> Delete(int id)
        {
            var entity = await _context.Adjustments.FindAsync(id);
            if (entity == null)
            {
                return entity;
            }
            _context.Adjustments.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Adjustment> GetById(int id)
        {
            return await _context.Adjustments.FindAsync(id);
        }

        public async Task<Adjustment> Update(Adjustment entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<Adjustment>> GetPaginated(int page, int pageSize)
        {
            return await _context.Adjustments.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        }
    }
}
