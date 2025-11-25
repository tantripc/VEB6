using System;
using System.Collections.Generic;

namespace MW.Entities;

public partial class DeliveryCode
{
    public int Id { get; set; }

    public string Code { get; set; } = null!;

    public bool? IsMapping { get; set; }

    public string? Description { get; set; }
}
