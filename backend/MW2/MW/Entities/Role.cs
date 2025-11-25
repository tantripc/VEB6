using System;
using System.Collections.Generic;

namespace MW.Entities;

public partial class Role
{
    public Guid Id { get; set; }

    public byte Type { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? Url { get; set; }

    public string? CreateBy { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public byte ActiveFlag { get; set; }

    public virtual ICollection<MenuRole> MenuRoles { get; set; } = new List<MenuRole>();

    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
