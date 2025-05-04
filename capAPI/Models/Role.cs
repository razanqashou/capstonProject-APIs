using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace capAPI.Models;

[Index("RoleNameEn", Name = "UQ__Roles__CB8E64700B89AB80", IsUnique = true)]
[Index("RoleNameAr", Name = "UQ__Roles__CB8F84E98C8D544F", IsUnique = true)]
public partial class Role
{
    [Key]
    [Column("RoleID")]
    public int RoleId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string RoleNameEn { get; set; } = null!;

    [StringLength(50)]
    public string RoleNameAr { get; set; } = null!;

    public bool? IsActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [StringLength(50)]
    public string? CreatedBy { get; set; }

    [StringLength(50)]
    public string? UpdatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [Column("OTP_Length")]
    public int OtpLength { get; set; }

    [Column("OTP_Expiry")]
    public int OtpExpiry { get; set; }

    [InverseProperty("Role")]
    public virtual ICollection<PermissionRole> PermissionRoles { get; set; } = new List<PermissionRole>();

    [InverseProperty("Role")]
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
