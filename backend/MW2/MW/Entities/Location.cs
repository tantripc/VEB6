using System;
using System.Collections.Generic;

namespace MW.Entities;

public partial class Location
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string CityCode { get; set; } = null!;

    public string CityName { get; set; } = null!;

    public string DistrictCode { get; set; } = null!;

    public string DistrictName { get; set; } = null!;

    public string WardCode { get; set; } = null!;

    public string WardName { get; set; } = null!;

    public int? OrderNumber { get; set; }

    public string Url { get; set; } = null!;

    public string? Description { get; set; }

    public string? CreateBy { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public byte ActiveFlag { get; set; }
}
