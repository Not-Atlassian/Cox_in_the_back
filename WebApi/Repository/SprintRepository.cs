using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Repository
{
    public class SprintRepository : IRepository<Sprint>
    {
        public readonly JyrosContext _context;

        public SprintRepository(JyrosContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Sprint>> GetAll()
        {
            return await _context.Sprints.ToListAsync();
        }

        public async Task<Sprint> Add(Sprint entity)
        {
            _context.Sprints.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Sprint> Delete(int id)
        {
            var entity = await _context.Sprints.FindAsync(id);
            if (entity == null)
            {
                return entity;
            }

            _context.Sprints.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Sprint> GetById(int id)
        {
            return await _context.Sprints.FindAsync(id);
        }

        public async Task<Sprint> Update(Sprint entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<Sprint>> GetPaginated(int page, int pageSize)
        {
            return await _context.Sprints.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        }
    }
}