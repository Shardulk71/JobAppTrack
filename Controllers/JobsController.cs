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
    
    
    
    
}