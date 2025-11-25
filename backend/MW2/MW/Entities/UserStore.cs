using System;
using System.Collections.Generic;

namespace MW.Entities;

public partial class UserStore
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public Guid StoreId { get; set; }

    public string? UserName { get; set; }

    public string? StoreCode { get; set; }

    public string? StoreName { get; set; }

    public string? Description { get; set; }

    public int? OrderNumber { get; set; }

    public string? Url { get; set; }

    public string? CreateBy { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public byte ActiveFlag { get; set; }
}
