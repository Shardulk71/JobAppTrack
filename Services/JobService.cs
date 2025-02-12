using JobAppTrack.Models;
using JobAppTrack.Repositories;

namespace JobAppTrack.Services;

public class JobService : IJobService
{
    private readonly IJobRepository _jobRepository;
    private readonly IJobSkillRepository _jobSkillRepository;

    public JobService(IJobRepository jobRepository, IJobSkillRepository jobSkillRepository)
    {
        _jobRepository = jobRepository;
        _jobSkillRepository = jobSkillRepository;
    }
    
    public async Task<Job?> GetJobAsync(int jobId, int userId)
    {
        var job = await _jobRepository.GetJobByIdAsync(jobId, userId);
        if (job == null) return null;

        // Fetch skills associated with the job
        job.JobSkills = (await _jobSkillRepository.GetJobSkillsByJobIdAsync(jobId, userId));
    
        return job;
    }
    
    public async Task CreateJobAsync(Job job, int userId)
    {
        job.UserId = userId;
        await _jobRepository.AddJobAsync(job);
        
        if (job.JobSkills.Count > 0)
        {
            foreach (var skill in job.JobSkills)
            {
                skill.JobId = job.JobId;  
                await _jobSkillRepository.AddJobSkillAsync(skill);
            }
        }

        await _jobRepository.SaveChangesAsync();
    }
    
    public async Task UpdateJobAsync(Job updatedJob, int userId)
    {
        var job = await _jobRepository.GetJobByIdAsync(updatedJob.JobId, userId);
        if (job == null) throw new Exception("Job not found");

        job.Title = updatedJob.Title;
        job.Company = updatedJob.Company;
        job.City = updatedJob.City;
        job.Country = updatedJob.Country;
        job.IsRemote = updatedJob.IsRemote;
        job.Description = updatedJob.Description;
        job.DatePosted = updatedJob.DatePosted;
        job.ClosingDate = updatedJob.ClosingDate;
        job.Salary = updatedJob.Salary;
        job.Type = updatedJob.Type;
        job.JobFunctionArea = updatedJob.JobFunctionArea;
        job.Level = updatedJob.Level;
        job.Notes = updatedJob.Notes;

        // Handle skills update
        var existingSkills = await _jobSkillRepository.GetJobSkillsByJobIdAsync(updatedJob.JobId, userId);
        var newSkills = job.JobSkills ?? new List<JobSkill>(); //?? ensures newSkills never becomes null by providing an empty list as a fallback.

        // Remove skills that are not in the updated list
        var skillsToRemove = existingSkills
            .Where(es => !newSkills.Select(ns => ns.Skill).Contains(es.Skill))
            .ToList();

        await _jobSkillRepository.RemoveJobSkillsAsync(skillsToRemove);
        await _jobSkillRepository.SaveChangesAsync();
        // Find skills that are in newSkills but not in existingSkills
        var skillsToAdd = newSkills.Where(ns => !existingSkills.Any(es => es.Skill == ns.Skill)).ToList();
        // Set the JobId for each new skill
        skillsToAdd.ForEach(skill => skill.JobId = job.JobId);
        // Add the new skills in one go
        await _jobSkillRepository.AddJobSkillsAsync(skillsToAdd);

        await _jobRepository.SaveChangesAsync();
    }
}