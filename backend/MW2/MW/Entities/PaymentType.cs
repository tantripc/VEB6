using System;
using System.Collections.Generic;

namespace MW.Entities;

public partial class PaymentType
{
    public int Id { get; set; }

    public string Type { get; set; } = null!;

    public string? Description { get; set; }

    public bool? IsMethod { get; set; }

    public int? Scope { get; set; }
}
