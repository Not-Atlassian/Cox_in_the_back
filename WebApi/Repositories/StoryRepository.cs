using Microsoft.EntityFrameworkCore;
using WebApi.Context;
using WebApi.Models;
using WebApi.RepositoryInterfaces;


namespace WebApi.Repositories
{
    public class StoryRepository : IStoryRepository
    {
        public readonly JyrosContext _context;
        public StoryRepository(JyrosContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Story>> GetAll()
        {
            return await _context.Stories.ToListAsync();
        }

        public async Task<Story> Add(Story entity)
        {
            _context.Stories.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Story> Delete(int id)
        {
            var entity = await _context.Stories.FindAsync(id);
            if (entity == null)
            {
                return entity;
            }

            _context.Stories.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Story> GetById(int id)
        {
            return await _context.Stories.FindAsync(id);
        }

        public async Task<Story> Update(Story entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<Story>> GetPaginated(int page, int pageSize)
        {
            return await _context.Stories.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<IEnumerable<Story>> GetFilteredPaginated(string searchKey, int page, int pageSize)
        {
            return await _context.Stories.Where(s => s.Title.Contains(searchKey)).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        }
    }
}