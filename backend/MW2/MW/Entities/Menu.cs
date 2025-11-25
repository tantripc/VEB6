using System;
using System.Collections.Generic;

namespace MW.Entities;

public partial class Menu
{
    public Guid Id { get; set; }

    public Guid? ParentId { get; set; }

    public byte MenuStatus { get; set; }

    public string? Controller { get; set; }

    public string? Action { get; set; }

    public string? ResourceId { get; set; }

    public string? NameVi { get; set; }

    public string? NameEn { get; set; }

    public string? Icon { get; set; }

    public int OrderNumber { get; set; }

    public string? Url { get; set; }

    public string? Description { get; set; }

    public string? Method { get; set; }

    public string? CreateBy { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public byte ActiveFlag { get; set; }
}
