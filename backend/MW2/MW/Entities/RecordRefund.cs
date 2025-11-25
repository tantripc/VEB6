using System;
using System.Collections.Generic;

namespace MW.Entities;

public partial class RecordRefund
{
    public Guid Id { get; set; }

    public Guid HeaderId { get; set; }

    public string StoreCode { get; set; } = null!;

    public string ReceiptNumber { get; set; } = null!;

    public double TotalAmount { get; set; }

    public double PromotionAmount { get; set; }

    public string? Url { get; set; }

    public string? CreateBy { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public byte ActiveFlag { get; set; }

    public bool IsTransfer { get; set; }

    public bool IsTransferSap { get; set; }

    public bool? IsTransferS4 { get; set; }
}
