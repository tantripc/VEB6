using System;
using System.Collections.Generic;

namespace MW.Entities;

public partial class Delivery
{
    public Guid Id { get; set; }

    public Guid HeaderId { get; set; }

    public string SubOrderNumber { get; set; } = null!;

    public string DeliveryCode { get; set; } = null!;

    public string? TrackingNumber { get; set; }

    public string? Url { get; set; }

    public string? CreateBy { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public byte ActiveFlag { get; set; }

    public virtual Header Header { get; set; } = null!;
}
