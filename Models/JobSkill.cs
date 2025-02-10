using System;
using System.Collections.Generic;

namespace JobAppTrack.Models;

public partial class JobSkill
{
    public int JobSkillId { get; set; }

    public int JobId { get; set; }

    public string Skill { get; set; } = null!;

    public virtual Job Job { get; set; } = null!;
}
