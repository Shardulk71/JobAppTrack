using JobAppTrack.Models;

namespace JobAppTrack.Services;

public interface IJobService
{
    Task<Job?> GetJobAsync(int jobId, int userId);
    Task CreateJobAsync(Job job, int userId);
    Task UpdateJobAsync(Job job, int userId);
    Task DeleteJobAsync(int jobId, int userId);
}