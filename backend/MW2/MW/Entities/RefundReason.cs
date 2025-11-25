using System;
using System.Collections.Generic;

namespace MW.Entities;

public partial class RefundReason
{
    public string ReasonCode { get; set; } = null!;

    public string ReasonName { get; set; } = null!;
}
