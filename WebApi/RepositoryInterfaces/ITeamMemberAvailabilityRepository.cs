using Microsoft.EntityFrameworkCore;
using WebApi.Context;
using WebApi.Models;

namespace WebApi.RepositoryInterfaces
{
    public interface ITeamMemberAvailabilityRepository : IRepository<TeamMemberAvailability>
    {
        Task<TeamMemberAvailability> GetBySprintIdAndUserId(int sprintId, int userId);
    }
}