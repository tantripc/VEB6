using System;
using System.Collections.Generic;

namespace MW.Entities;

public partial class UserDepartment
{
    public Guid UserId { get; set; }

    public Guid DeptId { get; set; }

    public string? JobDescription { get; set; }

    public int? OrderNumber { get; set; }

    public bool? IsManager { get; set; }

    public string? Url { get; set; }

    public string? CreateBy { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public byte ActiveFlag { get; set; }
}
