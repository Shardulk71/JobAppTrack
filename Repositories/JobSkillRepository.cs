using JobAppTrack.Models;
using Microsoft.EntityFrameworkCore;

namespace JobAppTrack.Repositories;

public class JobSkillRepository : IJobSkillRepository
{
    private readonly ApplicationDbContext _context;

    public JobSkillRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IList<JobSkill>> GetJobSkillsByJobIdAsync(int jobId, int userId)
    {
        // Verify that the job exists and belongs to the user.
        bool jobBelongsToUser = await _context.Jobs.AnyAsync(j => j.JobId == jobId && j.UserId == userId);
        if (!jobBelongsToUser)
        {
            return [];
        }

        return await _context.JobSkills
            .Where(js => js.JobId == jobId)
            .ToListAsync();
    }

    public async Task<JobSkill?> GetJobSkillByIdAsync(int jobSkillId, int jobId, int userId)
    {
        bool jobBelongsToUser = await _context.Jobs.AnyAsync(j => j.JobId == jobId && j.UserId == userId);
        if (!jobBelongsToUser)
        {
            return null;
        }

        return await _context.JobSkills
            .FirstOrDefaultAsync(js => js.JobSkillId == jobSkillId && js.JobId == jobId);
    }

    public async Task AddJobSkillAsync(JobSkill jobSkill)
    {
        await _context.JobSkills.AddAsync(jobSkill);
    }
    
    public async Task AddJobSkillsAsync(IList<JobSkill> jobSkills)
    {
        await _context.JobSkills.AddRangeAsync(jobSkills);
        await _context.SaveChangesAsync();
    }
    
    public async Task RemoveJobSkillsAsync(IList<JobSkill> jobSkills)
    {
        _context.JobSkills.RemoveRange(jobSkills);
        await _context.SaveChangesAsync();
    }
    
    public void DeleteJobSkill(JobSkill jobSkill)
    {
        _context.JobSkills.Remove(jobSkill);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}