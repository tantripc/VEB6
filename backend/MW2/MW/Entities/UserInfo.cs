using System;
using System.Collections.Generic;

namespace MW.Entities;

public partial class UserInfo
{
    public Guid Id { get; set; }

    public string UserId { get; set; } = null!;

    public string? FullName { get; set; }

    public string Email { get; set; } = null!;

    public string? Mobile { get; set; }

    public bool IsActive { get; set; }

    public byte[]? Picture { get; set; }

    public string? Address { get; set; }

    public string? HomePhone { get; set; }

    public string? Ext { get; set; }

    public DateOnly? Birthday { get; set; }

    public bool? Gender { get; set; }

    public string? LanguageCode { get; set; }

    public string? Url { get; set; }

    public string? CreateBy { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public byte ActiveFlag { get; set; }

    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
