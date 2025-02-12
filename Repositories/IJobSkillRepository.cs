using JobAppTrack.Models;

namespace JobAppTrack.Repositories;

public interface IJobSkillRepository
{
    Task<IList<JobSkill>> GetJobSkillsByJobIdAsync(int jobId, int userId);
    Task<JobSkill?> GetJobSkillByIdAsync(int jobSkillId, int jobId, int userId);
    Task AddJobSkillAsync(JobSkill jobSkill);
    void DeleteJobSkill(JobSkill jobSkill);
    Task SaveChangesAsync();
    Task AddJobSkillsAsync(IList<JobSkill> jobSkills);
    Task RemoveJobSkillsAsync(IList<JobSkill> jobSkills);
}