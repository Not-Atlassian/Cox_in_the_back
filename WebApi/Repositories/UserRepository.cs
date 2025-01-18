using Microsoft.EntityFrameworkCore;
using WebApi.Context;
using WebApi.Models;
using WebApi.RepositoryInterfaces;

namespace WebApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly JyrosContext _context;
        public UserRepository(JyrosContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }


        public async Task<User> Add(User entity)
        {
            try
            {
                entity.UserId =default;
            }
            catch (System.Exception)
            {
                throw;
            }
            _context.Users.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<User> Delete(int id)
        {
            var entity = await _context.Users.FindAsync(id);
            if (entity == null)
            {
                return entity;
            }

            _context.Users.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<User> GetById(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> Update(User entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<User>> GetPaginated(int page, int pageSize)
        {
            return await _context.Users.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        }
        public async Task<IEnumerable<User>> GetUsersBySprintId(int sprintId)
        {
            return await _context.TeamMemberAvailabilities
                .Where(tma => tma.SprintId == sprintId)
                .Join(_context.Users,
                    tma => tma.UserId,
                    user => user.UserId,
                    (tma, user) => user)
                .ToListAsync();
        }
        public async Task<User> GetUserByName(string name)
        {
            return await _context.Users.Where(u => u.Username == name).FirstAsync();
        }
    }

}