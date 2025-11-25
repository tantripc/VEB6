using System;
using System.Collections.Generic;

namespace MW.Entities;

public partial class RefundHeader
{
    public Guid Id { get; set; }

    public string MallCode { get; set; } = null!;

    public string RefundDate { get; set; } = null!;

    public string RefundTime { get; set; } = null!;

    public string OrderNumber { get; set; } = null!;

    public string SalesDate { get; set; } = null!;

    public string ReasonCode { get; set; } = null!;

    public string? Description { get; set; }

    public bool? IsTransfer { get; set; }

    public string? Url { get; set; }

    public string CreateBy { get; set; } = null!;

    public string? UpdateBy { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public byte ActiveFlag { get; set; }

    public string? CustomerType { get; set; }

    public string? ActualOrderNumber { get; set; }

    public string CustomerId { get; set; } = null!;

    public string? FoxtrotUserId { get; set; }

    public virtual ICollection<RefundItem> RefundItems { get; set; } = new List<RefundItem>();

    public virtual ICollection<RefundPayment> RefundPayments { get; set; } = new List<RefundPayment>();
}
