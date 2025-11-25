using System;
using System.Collections.Generic;

namespace MW.Entities;

public partial class Payment
{
    public Guid Id { get; set; }

    public Guid HeaderId { get; set; }

    public string PaymentType { get; set; } = null!;

    public double TotalAmount { get; set; }

    public string? TransactionId { get; set; }

    public string? AuthId { get; set; }

    public double TotalAmountWithoutVatforTaxableItems { get; set; }

    public double TotalAmountForNonTaxableItems { get; set; }

    public double TotalTaxAmount { get; set; }

    public string? Url { get; set; }

    public string? CreateBy { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public byte ActiveFlag { get; set; }

    public string? SubOrderId { get; set; }

    public virtual Header Header { get; set; } = null!;
}
