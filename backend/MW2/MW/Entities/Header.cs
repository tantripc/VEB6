using System;
using System.Collections.Generic;

namespace MW.Entities;

public partial class Header
{
    public Guid Id { get; set; }

    public string StoreCode { get; set; } = null!;

    public string FulfillmentDate { get; set; } = null!;

    public string SettlementTime { get; set; } = null!;

    public string OrderNumber { get; set; } = null!;

    public bool? IsTransfer { get; set; }

    public string? Url { get; set; }

    public string? CreateBy { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public byte ActiveFlag { get; set; }

    public string? CustomerType { get; set; }

    public string? ActualOrderNumber { get; set; }

    public string CustomerId { get; set; } = null!;

    public string? FoxtrotUserId { get; set; }

    public virtual ICollection<Delivery> Deliveries { get; set; } = new List<Delivery>();

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    public virtual ICollection<Item> Items { get; set; } = new List<Item>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
