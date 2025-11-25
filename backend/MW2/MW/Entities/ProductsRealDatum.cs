using System;
using System.Collections.Generic;

namespace MW.Entities;

public partial class ProductsRealDatum
{
    public string? Sku { get; set; }

    public string? StoreCode { get; set; }

    public DateTime? StartPublishDate { get; set; }
}
