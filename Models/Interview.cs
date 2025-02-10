using System;
using System.Collections.Generic;

namespace JobAppTrack.Models;

public partial class Interview
{
    public int InterviewId { get; set; }

    public string Stage { get; set; } = null!;

    public DateOnly Date { get; set; }

    public TimeOnly? Time { get; set; }

    public string? Interviewer { get; set; }

    public string? Type { get; set; }

    public int AppId { get; set; }

    public string? Notes { get; set; }

    public string? Outcome { get; set; }

    public int UserId { get; set; }

    public virtual Application App { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
