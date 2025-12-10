using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiddlewareTool.Entities.Models;

public partial class Resource
{
    [Key]
    [StringLength(250)]
    public string ResourceID { get; set; }

    [StringLength(400)]
    public string ResourceText0 { get; set; }

    [StringLength(400)]
    public string DefaultText0 { get; set; }

    [StringLength(400)]
    public string ResourceText1 { get; set; }

    [StringLength(400)]
    public string DefaultText1 { get; set; }

    [StringLength(400)]
    public string ResourceText2 { get; set; }

    [StringLength(400)]
    public string DefaultText2 { get; set; }

    [StringLength(400)]
    public string ResourceText3 { get; set; }

    [StringLength(400)]
    public string DefaultText3 { get; set; }

    [StringLength(400)]
    public string ResourceText4 { get; set; }

    [StringLength(400)]
    public string DefaultText4 { get; set; }

    [StringLength(400)]
    public string ResourceText5 { get; set; }

    [StringLength(400)]
    public string DefaultText5 { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreateDate { get; set; }
}
