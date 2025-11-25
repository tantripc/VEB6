using System;
using System.Collections.Generic;

namespace MW.Entities;

public partial class History
{
    public Guid Id { get; set; }

    public Guid HeaderId { get; set; }

    public string? ActionCode { get; set; }

    public string? Comment { get; set; }

    public string? Description { get; set; }

    public DateTime CreatedDate { get; set; }

    public string UserId { get; set; } = null!;

    public string? Log { get; set; }

    public virtual Header1 Header { get; set; } = null!;
}
