using System;
using System.Collections.Generic;

namespace MW.Entities;

public partial class SystemLog
{
    public Guid LogId { get; set; }

    public string? Module { get; set; }

    public string? UserId { get; set; }

    public int? UserFunction { get; set; }

    public int? EventResult { get; set; }

    public DateTime? FuncDateTime { get; set; }

    public string? Source { get; set; }

    public string? Transdata { get; set; }

    public string? Wsname { get; set; }
}
