using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace capAPI.Models;

public partial class PermissionRole
{
    [Key]
    [Column("PermissionRoleID")]
    public int PermissionRoleId { get; set; }

    [Column("RoleID")]
    public int RoleId { get; set; }

    [Column("PermissionID")]
    public int PermissionId { get; set; }

    [ForeignKey("PermissionId")]
    [InverseProperty("PermissionRoles")]
    public virtual Permission Permission { get; set; } = null!;

    [ForeignKey("RoleId")]
    [InverseProperty("PermissionRoles")]
    public virtual Role Role { get; set; } = null!;
}
