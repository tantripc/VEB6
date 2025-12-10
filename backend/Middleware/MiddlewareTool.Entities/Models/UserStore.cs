using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiddlewareTool.Entities.Models;

[PrimaryKey("UserId", "StoreId")]
[Table("UserStores", Schema = "acc")]
[Index("ActiveFlag", "UserName", "UserId", "StoreId", Name = "IX_UserStores")]
public partial class UserStore
{
    public Guid Id { get; set; }

    [Key]
    public Guid UserId { get; set; }

    [Key]
    public Guid StoreId { get; set; }

    [StringLength(150)]
    public string UserName { get; set; }

    [StringLength(150)]
    public string StoreCode { get; set; }

    [StringLength(150)]
    public string StoreName { get; set; }

    [StringLength(150)]
    public string Description { get; set; }

    public int? OrderNumber { get; set; }

    public string URL { get; set; }

    [StringLength(50)]
    public string CreateBy { get; set; }

    [StringLength(50)]
    public string UpdateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreateDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime UpdateDate { get; set; }

    public byte ActiveFlag { get; set; }
}
