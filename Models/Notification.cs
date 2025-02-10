using System;
using System.Collections.Generic;

namespace JobAppTrack.Models;

public partial class Notification
{
    public int NotifId { get; set; }

    public int AppId { get; set; }

    public string Message { get; set; } = null!;

    public int UserId { get; set; }

    public virtual Application App { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
