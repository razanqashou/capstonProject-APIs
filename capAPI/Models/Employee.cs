using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace capAPI.Models;

public partial class Employee
{
    [Key]
    [Column("EmployeeID")]
    public int EmployeeId { get; set; }

    public int EmployeeType { get; set; }

    public bool? IsActive { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string? CreatedBy { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string? UpdatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [Column("userId")]
    public int? UserId { get; set; }

    [InverseProperty("Employee")]
    public virtual Driver? Driver { get; set; }

    [ForeignKey("EmployeeType")]
    [InverseProperty("Employees")]
    public virtual LookupValue EmployeeTypeNavigation { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("Employees")]
    public virtual User? User { get; set; }
}
