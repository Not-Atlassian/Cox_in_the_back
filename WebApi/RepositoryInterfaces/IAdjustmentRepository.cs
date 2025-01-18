

using WebApi.Models;

namespace WebApi.RepositoryInterfaces
{
    public interface IAdjustmentRepository : IRepository<Adjustment>
    {
        Task<int> GetAdjustmentsPerSprint(int sprintId);
    }
}
