using System;
using System.Collections.Generic;

namespace JobAppTrack.Models;

public partial class UserSkill
{
    public int SkillId { get; set; }

    public int UserId { get; set; }

    public string Skill { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
