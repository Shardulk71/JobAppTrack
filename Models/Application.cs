using System;
using System.Collections.Generic;

namespace JobAppTrack.Models;

public partial class Application
{
    public int AppId { get; set; }

    public int JobId { get; set; }

    public int UserId { get; set; }

    public DateTime? ApplicationDate { get; set; }

    public string? Resume { get; set; }

    public string? CoverLetter { get; set; }

    public string Status { get; set; } = null!;

    public int? MatchPercent { get; set; }

    public virtual ICollection<Interview> Interviews { get; set; } = new List<Interview>();

    public virtual Job Job { get; set; } = null!;

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual User User { get; set; } = null!;
}
