using System.Collections.Generic;
using System.Threading.Tasks;
using LoginExampleServer.Models;

namespace LoginExampleServer.Data
{
    public interface IJobsService
    {
        Task<IList<Job>> GetJobsAsync();
        Task AddJobAsync(Job job);
        Task RemoveJobAsync(int jobId);
    }
}