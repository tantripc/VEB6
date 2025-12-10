using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiddlewareTool.Entities.Models;

[Index("EventResult", Name = "IX_EventResult")]
[Index("FuncDateTime", Name = "IX_FuncDateTime", AllDescending = true)]
[Index("Module", Name = "IX_Module")]
[Index("FuncDateTime", "Module", "UserFunction", "EventResult", "UserId", Name = "IX_SystemLogs", IsDescending = new[] { true, false, false, true, false })]
[Index("UserFunction", Name = "IX_UserFunction")]
public partial class SystemLog
{
    [Key]
    public Guid LogId { get; set; }

    [StringLength(100)]
    public string Module { get; set; }

    [StringLength(50)]
    public string UserId { get; set; }

    public int? UserFunction { get; set; }

    public int? EventResult { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? FuncDateTime { get; set; }

    public string Source { get; set; }

    public string Transdata { get; set; }

    public string WSName { get; set; }
}
