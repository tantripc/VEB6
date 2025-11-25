using System;
using System.Collections.Generic;

namespace MW.Entities;

public partial class MenuRole
{
    public Guid Id { get; set; }

    public byte Type { get; set; }

    public Guid RoleId { get; set; }

    public Guid MenuId { get; set; }

    public Guid? MenuActionId { get; set; }

    public string? UserName { get; set; }

    public string? Url { get; set; }

    public string? CreateBy { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public byte ActiveFlag { get; set; }

    public virtual Role Role { get; set; } = null!;
}
