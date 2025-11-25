using System;
using System.Collections.Generic;

namespace MW.Entities;

public partial class RefundHeader1
{
    public Guid Id { get; set; }

    public Guid SaleOrderId { get; set; }

    public string StoreCode { get; set; } = null!;

    public DateTime RefundDate { get; set; }

    public string? RefundTime { get; set; }

    public string? OrderNumber { get; set; }

    public byte StatusId { get; set; }

    public string SalesDate { get; set; } = null!;

    public string ReasonCode { get; set; } = null!;

    public Guid? UploadId { get; set; }

    public decimal TotalVatamount { get; set; }

    public decimal TotalAmountWithoutVat { get; set; }

    public decimal TotalAmountWithVat { get; set; }

    public string? Description { get; set; }

    public string? Url { get; set; }

    public string CreateBy { get; set; } = null!;

    public string? UpdateBy { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public byte ActiveFlag { get; set; }

    public string CustomerName { get; set; } = null!;

    public string CustomerEmail { get; set; } = null!;

    public virtual ICollection<RefundInvoice1> RefundInvoice1s { get; set; } = new List<RefundInvoice1>();

    public virtual ICollection<RefundItem1> RefundItem1s { get; set; } = new List<RefundItem1>();

    public virtual Header1 SaleOrder { get; set; } = null!;
}
