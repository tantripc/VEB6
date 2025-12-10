using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiddlewareTool.Entities.Models;

[Table("UserInfos", Schema = "acc")]
[Index("UserId", Name = "IX_UserInfo")]
public partial class UserInfo
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [StringLength(200)]
    public string UserId { get; set; }

    [StringLength(300)]
    public string FullName { get; set; }

    [Required]
    [StringLength(100)]
    public string Email { get; set; }

    [StringLength(20)]
    public string Mobile { get; set; }

    public bool IsActive { get; set; }

    public byte[] Picture { get; set; }

    [StringLength(500)]
    public string Address { get; set; }

    [StringLength(20)]
    public string HomePhone { get; set; }

    [StringLength(10)]
    public string Ext { get; set; }

    public DateOnly? Birthday { get; set; }

    public bool? Gender { get; set; }

    [StringLength(50)]
    public string LanguageCode { get; set; }

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

    [InverseProperty("User")]
    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
