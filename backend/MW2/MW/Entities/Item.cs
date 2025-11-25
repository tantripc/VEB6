using System;
using System.Collections.Generic;

namespace MW.Entities;

public partial class Item
{
    public Guid Id { get; set; }

    public Guid HeaderId { get; set; }

    public string Sku { get; set; } = null!;

    public int QuantitySold { get; set; }

    public double SellingPrice { get; set; }

    public string StoreCode { get; set; } = null!;

    public double ListPrice { get; set; }

    public double Vatamount { get; set; }

    public double Vatcode { get; set; }

    public string? Url { get; set; }

    public string? CreateBy { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public byte ActiveFlag { get; set; }

    public virtual Header Header { get; set; } = null!;

    public virtual ICollection<Promotion> Promotions { get; set; } = new List<Promotion>();
}
