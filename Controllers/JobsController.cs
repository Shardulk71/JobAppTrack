using JobAppTrack.Models;
using JobAppTrack.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobAppTrack.Controllers;

[Route("api/[controller]")]
[ApiController]
public class JobsController :  ControllerBase
{
    private readonly IJobRepository _jobRepository;

    public JobsController(IJobRepository jobRepository)
    {
        _jobRepository = jobRepository;
    }
    
    //GET
    [HttpGet("getJobs")]
    public async Task<ActionResult<IEnumerable<Job>>> GetJobs()
    {
        int userId = 999 /*GetUserId() exract user id from claims*/;
        var jobs = await _jobRepository.GetJobsByUserIdAsync(userId);
        return Ok(jobs);
    }
    
    [HttpGet("getJob/{id}")]
    public async Task<ActionResult<Job>> GetJobById(int jobId)
    {
        int userId = 999 /*GetUserId() exract user id from claims*/;
        var job = await _jobRepository.GetJobByIdAsync(jobId, userId);
        if (job == null)
        {
            return NotFound();
        }
        return Ok(job);
    }
    
    
    
    //POST
    [HttpPost("createJob")]
    public async Task<ActionResult<Job>> CreateJob(Job job)
    {
        int userId = 999 /*GetUserId() exract user id from claims*/;
        job.UserId = userId; 
        await _jobRepository.AddJobAsync(job);
        await _jobRepository.SaveChangesAsync();
        return StatusCode(StatusCodes.Status201Created);
    }
    
    [HttpPut("updateJob/{jobId}")]
    public async Task<IActionResult> UpdateJob([FromRoute] int jobId, [FromBody] Job updatedJob)
    {
        int userId = 999 /*GetUserId() exract user id from claims*/;

        var job = await _jobRepository.GetJobByIdAsync(jobId, userId);
        if (job == null)
        {
            return NotFound("Job not found for the current user.");
        }
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

        await _jobRepository.SaveChangesAsync();
        return NoContent(); 
    }
    
    [HttpDelete("deleteJob/{jobId}")]
    public async Task<IActionResult> DeleteJob([FromRoute] int jobId)
    {
        int userId = 999 /*GetUserId() exract user id from claims*/;

        var job = await _jobRepository.GetJobByIdAsync(jobId, userId);
        if (job == null)
        {
            return NotFound("Job not found for the current user.");
        }

        _jobRepository.DeleteJob(job);
        await _jobRepository.SaveChangesAsync();
        return NoContent(); 
    }
    
    
    
}