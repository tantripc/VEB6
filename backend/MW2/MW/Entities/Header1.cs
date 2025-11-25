using System;
using System.Collections.Generic;

namespace MW.Entities;

public partial class Header1
{
    public Guid Id { get; set; }

    public Guid BusinessId { get; set; }

    public string StoreCode { get; set; } = null!;

    public string CustomerName { get; set; } = null!;

    public string CustomerEmail { get; set; } = null!;

    public string? OrderNumber { get; set; }

    public byte StatusId { get; set; }

    public string? Url { get; set; }

    public string? CreateBy { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public byte ActiveFlag { get; set; }

    public string? Description { get; set; }

    public DateTime ReceiptDate { get; set; }

    public Guid? UploadId { get; set; }

    public decimal TotalVatamount { get; set; }

    public decimal TotalAmountWithoutVat { get; set; }

    public decimal TotalAmountWithVat { get; set; }

    public string? ErrorMess { get; set; }

    public virtual Business Business { get; set; } = null!;

    public virtual ICollection<History> Histories { get; set; } = new List<History>();

    public virtual ICollection<Invoice1> Invoice1s { get; set; } = new List<Invoice1>();

    public virtual ICollection<Item1> Item1s { get; set; } = new List<Item1>();

    public virtual ICollection<RefundHeader1> RefundHeader1s { get; set; } = new List<RefundHeader1>();
}
