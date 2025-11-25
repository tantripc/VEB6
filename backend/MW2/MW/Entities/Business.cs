using System;
using System.Collections.Generic;

namespace MW.Entities;

public partial class Business
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string TaxName { get; set; } = null!;

    public string TaxCode { get; set; } = null!;

    public string TaxAddress { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Phone { get; set; }

    public string? Fax { get; set; }

    public string? Url { get; set; }

    public string? CreateBy { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public byte ActiveFlag { get; set; }

    public string? PayMethodCode { get; set; }

    public string? CustomerName { get; set; }

    public string? NoStreet { get; set; }

    public string? City { get; set; }

    public string? District { get; set; }

    public string? Ward { get; set; }

    public virtual ICollection<Header1> Header1s { get; set; } = new List<Header1>();
}
