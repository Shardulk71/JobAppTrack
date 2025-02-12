using JobAppTrack.Models;
using Microsoft.EntityFrameworkCore;

namespace JobAppTrack.Repositories;

public class ApplicationRepository : IApplicationRepository
{
    private readonly ApplicationDbContext _context;

    public ApplicationRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Application?>> GetApplicationsByUserIdAsync(int userId)
    {
        return await _context.Applications
            .Where(a => a.UserId == userId)
            .ToListAsync();
    }

    public async Task<Application?> GetApplicationByJobIdAsync(int jobId, int userId)
    {
        return await _context.Applications
            .FirstOrDefaultAsync(a => a.JobId == jobId && a.UserId == userId);
    }

    public async Task AddApplicationAsync(Application application)
    {
        await _context.Applications.AddAsync(application);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
    
    public void DeleteApplication(Application application)
    { 
        _context.Applications.Remove(application);
    }
}