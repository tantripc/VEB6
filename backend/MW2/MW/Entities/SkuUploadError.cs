using System;
using System.Collections.Generic;

namespace MW.Entities;

public partial class SkuUploadError
{
    public Guid Id { get; set; }

    public Guid UploadId { get; set; }

    public string? MallCode { get; set; }

    public string? StoreCode { get; set; }

    public string? ExpressLocationGroupName { get; set; }

    public string? Sku { get; set; }

    public string? Infor { get; set; }

    public int? OrderNumber { get; set; }

    public string Url { get; set; } = null!;

    public string? Description { get; set; }

    public string? CreateBy { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public byte ActiveFlag { get; set; }

    public string? ParcelLocationGroupName { get; set; }

    public virtual SkuUploadMonitor Upload { get; set; } = null!;
}
