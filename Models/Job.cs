using System;
using System.Collections.Generic;

namespace JobAppTrack.Models;

public partial class Job
{
    public int JobId { get; set; }

    public string Title { get; set; } = null!;

    public string Company { get; set; } = null!;

    public string? City { get; set; }

    public string? Country { get; set; }

    public bool? IsRemote { get; set; }

    public string Description { get; set; } = null!;

    public DateOnly? DatePosted { get; set; }

    public DateOnly? ClosingDate { get; set; }

    public string? Salary { get; set; }

    public string? Type { get; set; }

    public string? JobFunctionArea { get; set; }

    public int UserId { get; set; }

    public string? Level { get; set; }

    public string? Notes { get; set; }

    public virtual ICollection<Application> Applications { get; set; } = new List<Application>();

    public virtual ICollection<JobSkill> JobSkills { get; set; } = new List<JobSkill>();

    public virtual User User { get; set; } = null!;
}
