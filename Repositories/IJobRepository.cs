using JobAppTrack.Models;

namespace JobAppTrack.Repositories;

public interface IJobRepository
{
    Task<IEnumerable<Job>> GetJobsByUserIdAsync(int userId);
    Task<Job?> GetJobByIdAsync(int jobId, int userId);
    Task AddJobAsync(Job job);
    Task SaveChangesAsync();
    void DeleteJob(Job job);
}