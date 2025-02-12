using JobAppTrack.Models;
using JobAppTrack.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace JobAppTrack.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ApplicationsController : ControllerBase
{
    private readonly IApplicationRepository _appRepo;
    private readonly IJobRepository _jobRepo;

    public ApplicationsController(IApplicationRepository appRepo, IJobRepository jobRepo)
    {
        _appRepo = appRepo;
        _jobRepo = jobRepo;
    }
    
    //GET
    [HttpGet("getApplications")]
    public async Task<ActionResult<IEnumerable<Application>>> GetApplications()
    {
        int userId = 999 /*GetUserId() exract user id from claims*/;
        var applications = await _appRepo.GetApplicationsByUserIdAsync(userId);
        return Ok(applications);
    }
    
    [HttpGet("getApplication/{jobId}")]
    public async Task<ActionResult<Application>> GetApplicationByJobId(int jobId)
    {
        int userId = 999 /*GetUserId() exract user id from claims*/;
        var application = await _appRepo.GetApplicationByJobIdAsync(jobId, userId);
        if (application == null)
            return NotFound();
        return Ok(application);
    }
    
    //POST
    [HttpPost("apply/{jobId}")]
    public async Task<ActionResult<Application>> ApplyForJob(int jobId)
    {
        int userId = 999 /*GetUserId() exract user id from claims*/;
        // Ensure the job exists and belongs to the current user.
        var job = await _jobRepo.GetJobByIdAsync(jobId, userId);
        if (job == null)
        {
            return BadRequest("Job not found for the current user.");
        }

        // Check if an application already exists.
        var existingApp = await _appRepo.GetApplicationByJobIdAsync(jobId, userId);
        if (existingApp != null)
        {
            return BadRequest("Application already exists for this job.");
        }

        // Create a new application record.
        Application application = new Application
        {
            JobId = jobId,
            UserId = userId,
            ApplicationDate = DateTime.UtcNow,
            Status = "Applied"
        };

        await _appRepo.AddApplicationAsync(application);
        await _appRepo.SaveChangesAsync();

        return CreatedAtAction(nameof(GetApplicationByJobId), new { jobId = application.JobId }, application);
    }
    
    //PUT
    [HttpPut("updateApplication/{jobId}")]
    public async Task<IActionResult> UpdateApplicationForJob([FromRoute] int jobId, [FromBody] Application updatedApplication)
    {
        int userId = 999 /*GetUserId() exract user id from claims*/;

        // Retrieve the application for the given job that belongs to the current user.
        var application = await _appRepo.GetApplicationByJobIdAsync(jobId, userId);
        if (application == null)
        {
            return NotFound("Application not found for the current user.");
        }

        application.Status = updatedApplication.Status;
        application.CoverLetter = updatedApplication.CoverLetter;
        application.Resume = updatedApplication.Resume; 
        
        await _appRepo.SaveChangesAsync();
        return NoContent(); // HTTP 204
    }
    
    [HttpDelete("deleteApplication/{jobId}")]
    public async Task<IActionResult> DeleteApplicationForJob([FromRoute] int jobId)
    {
        int userId = 999 /*GetUserId() exract user id from claims*/;
        var application = await _appRepo.GetApplicationByJobIdAsync(jobId, userId);
        if (application == null)
        {
            return NotFound("Application not found for the current user.");
        }
        
        _appRepo.DeleteApplication(application);
        await _appRepo.SaveChangesAsync();
        return NoContent(); // HTTP 204
    }
}