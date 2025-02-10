using JobAppTrack.Models;
using Microsoft.EntityFrameworkCore;

namespace JobAppTrack.Repositories;

public class JobRepository : IJobRepository
{
    private readonly ApplicationDbContext _context;

    public JobRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Job>> GetJobsByUserIdAsync(int userId)
    {
        return await _context.Jobs
            .Where(j => j.UserId == userId)
            .ToListAsync();
    }

    public async Task<Job?> GetJobByIdAsync(int jobId, int userId)
    {
        return await _context.Jobs
            .FirstOrDefaultAsync(j => j.JobId == jobId && j.UserId == userId);
    }

    public async Task AddJobAsync(Job job)
    {
        await _context.Jobs.AddAsync(job);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}