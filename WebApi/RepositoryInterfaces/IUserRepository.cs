using Microsoft.EntityFrameworkCore;
using WebApi.Context;
using WebApi.Models;

namespace WebApi.RepositoryInterfaces
{
    public interface IUserRepository : IRepository<User>
    {
        public Task<IEnumerable<User>> GetUsersBySprintId(int sprintId);
        public Task<User> GetUserByName (string name);

    }


}