using System;
using System.Collections.Generic;

namespace MW.Entities;

public partial class Resource
{
    public string ResourceId { get; set; } = null!;

    public string? ResourceText0 { get; set; }

    public string? DefaultText0 { get; set; }

    public string? ResourceText1 { get; set; }

    public string? DefaultText1 { get; set; }

    public string? ResourceText2 { get; set; }

    public string? DefaultText2 { get; set; }

    public string? ResourceText3 { get; set; }

    public string? DefaultText3 { get; set; }

    public string? ResourceText4 { get; set; }

    public string? DefaultText4 { get; set; }

    public string? ResourceText5 { get; set; }

    public string? DefaultText5 { get; set; }

    public DateTime CreateDate { get; set; }
}
