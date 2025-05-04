using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace capAPI.Models;

[Index("EmployeeId", Name = "UQ__Drivers__C134C9C015E6C061", IsUnique = true)]
public partial class Driver
{
    [Key]
    [Column("DriverID")]
    public int DriverId { get; set; }

    public int? VehicleType { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? VehiclePlateNumber { get; set; }

    [Column("isActive")]
    public bool? IsActive { get; set; }

    public int? CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    public int? UpdatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [Column("employeeId")]
    public int? EmployeeId { get; set; }

    [ForeignKey("CreatedBy")]
    [InverseProperty("Drivers")]
    public virtual User? CreatedByNavigation { get; set; }

    [InverseProperty("Driver")]
    public virtual ICollection<DriverRate> DriverRates { get; set; } = new List<DriverRate>();

    [ForeignKey("EmployeeId")]
    [InverseProperty("Driver")]
    public virtual Employee? Employee { get; set; }

    [InverseProperty("Driver")]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    [InverseProperty("Driver")]
    public virtual ICollection<Trip> Trips { get; set; } = new List<Trip>();

    [ForeignKey("VehicleType")]
    [InverseProperty("Drivers")]
    public virtual LookupValue? VehicleTypeNavigation { get; set; }
}
