using System;
using System.Collections.Generic;

namespace MW.Entities;

public partial class PaymentByStore
{
    public Guid Id { get; set; }

    public Guid HeaderId { get; set; }

    public string StoreCode { get; set; } = null!;

    public string PaymentType { get; set; } = null!;

    public double AmountRefund { get; set; }

    public string? TransactionId { get; set; }

    public string? AuthorizationId { get; set; }

    public string? UserId { get; set; }

    public string? Url { get; set; }

    public string? CreateBy { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public byte ActiveFlag { get; set; }
}
