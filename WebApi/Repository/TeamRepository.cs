using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Repository
{
    public class TeamRepository : IRepository<Team>
    {

        private readonly JyrosContext _context;
        public TeamRepository(JyrosContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Team>> GetAll()
        {
            return await _context.Teams.ToListAsync();
        }

        public async Task<Team> Add(Team entity)
        {
            _context.Teams.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Team> Delete(int id)
        {
            var entity = await _context.Teams.FindAsync(id);
            if (entity == null)
            {
                return entity;
            }

            _context.Teams.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Team> GetById(int id)
        {
            return await _context.Teams.FindAsync(id);
        }

        public async Task<Team> Update(Team entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<Team>> GetPaginated(int page, int pageSize)
        {
            return await _context.Teams.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        }

    }
}