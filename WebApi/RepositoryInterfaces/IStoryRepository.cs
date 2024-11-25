using Microsoft.EntityFrameworkCore;
using WebApi.Context;
using WebApi.Models;

namespace WebApi.RepositoryInterfaces
{
    public interface IStoryRepository : IRepository<Story>
    {
        public Task<IEnumerable<Story>> GetFilteredPaginated(string searchKey, int page, int pageSize);
    }
}