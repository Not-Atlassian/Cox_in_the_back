using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WebApi.Context;
using WebApi.Models;
using WebApi.RepositoryInterfaces;

namespace WebApi.Repositories
{
    public class TeamMemberAvailabilityRepository : ITeamMemberAvailabilityRepository
    {
        private readonly JyrosContext _context;
        public TeamMemberAvailabilityRepository(JyrosContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<TeamMemberAvailability>> GetAll()
        {
            return await _context.TeamMemberAvailabilities.ToListAsync();
        }
        public async Task<TeamMemberAvailability> Add(TeamMemberAvailability entity)
        {
            _context.TeamMemberAvailabilities.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        public async Task<TeamMemberAvailability> Delete(int id)
        {
            var entity = await _context.TeamMemberAvailabilities.FindAsync(id);
            if (entity == null)
            {
                return entity;
            }
            _context.TeamMemberAvailabilities.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        public async Task<TeamMemberAvailability> GetById(int id)
        {
            return await _context.TeamMemberAvailabilities.FindAsync(id);
        }
        public async Task<TeamMemberAvailability> Update(TeamMemberAvailability entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<TeamMemberAvailability>> GetPaginated(int page, int pageSize)
        {
            return await _context.TeamMemberAvailabilities.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<TeamMemberAvailability> GetBySprintIdAndUserId(int sprintId, int userId)
        {
            return await _context.TeamMemberAvailabilities.FirstOrDefaultAsync(tma => tma.SprintId == sprintId && tma.UserId == userId);
        }

        public async Task<TeamMemberAvailability> GetAvailabilityBySprintIdAndUserId(int sprintId, int userId)
        {
            return await _context.TeamMemberAvailabilities.FirstOrDefaultAsync(tma => tma.SprintId == sprintId && tma.UserId == userId);
        }

        public async Task<int> GetTotalAvailabilityPerSprint(int sprintId)
        {
            return await _context.TeamMemberAvailabilities
                .Where(tma => tma.SprintId == sprintId)
                .SumAsync(tma => tma.AvailabilityPoints);
        }
    }
}