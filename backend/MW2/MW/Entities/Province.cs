using System;
using System.Collections.Generic;

namespace MW.Entities;

public partial class Province
{
    public string CityCode { get; set; } = null!;

    public string CityName { get; set; } = null!;

    public string DistrictCode { get; set; } = null!;

    public string DistrictName { get; set; } = null!;

    public string WardCode { get; set; } = null!;

    public string WardName { get; set; } = null!;

    public string? Level { get; set; }

    public string? EnglishName { get; set; }
}
