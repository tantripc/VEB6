using System;
using System.Collections.Generic;

namespace MW.Entities;

public partial class Mall
{
    public Guid Id { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public int? OrderNumber { get; set; }

    public string Url { get; set; } = null!;

    public string? Description { get; set; }

    public string? CreateBy { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public byte ActiveFlag { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? AddressLine { get; set; }

    public string? CityName { get; set; }

    public string? CityCode { get; set; }

    public string? DistrictName { get; set; }

    public string? DistrictCode { get; set; }

    public string? WardName { get; set; }

    public string? WardCode { get; set; }

    public string? MerchantId { get; set; }
}
