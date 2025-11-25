using System;
using System.Collections.Generic;

namespace MW.Entities;

public partial class ProductHistoryAction
{
    public Guid Id { get; set; }

    public string Action { get; set; } = null!;

    public byte Value { get; set; }

    public string? Name { get; set; }
}
