using JobAppTrack.Models;

namespace JobAppTrack.Repositories;

public interface IApplicationRepository
{
    Task<IEnumerable<Application?>> GetApplicationsByUserIdAsync(int userId);
    Task<Application?> GetApplicationByJobIdAsync(int jobId, int userId);
    Task AddApplicationAsync(Application application);
    Task SaveChangesAsync();
    void DeleteApplication(Application application);
}