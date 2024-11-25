using Microsoft.EntityFrameworkCore;
using WebApi.Context;
using WebApi.Models;

namespace WebApi.RepositoryInterfaces
{
    public interface ITeamRepository : IRepository<Team>
    {
    }
}