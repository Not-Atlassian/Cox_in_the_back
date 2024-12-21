using Microsoft.EntityFrameworkCore;
using WebApi.Context;
using WebApi.Models;

namespace WebApi.RepositoryInterfaces
{
    public interface IUserRepository : IRepository<User>
    {
        public Task<int> GetAvailabilityPoints(int id);
        public Task<IEnumerable<UserGetDTO>> GetAllGood();

    }


}