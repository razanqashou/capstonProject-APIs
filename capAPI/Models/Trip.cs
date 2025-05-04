using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace capAPI.Models;

public partial class Trip
{
    [Key]
    [Column("TripID")]
    public int TripId { get; set; }

    [Column("DriverID")]
    public int DriverId { get; set; }

    [Column("TripStatusID")]
    public int TripStatusId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? StartTime { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? EndTime { get; set; }

    public int CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [StringLength(1)]
    public string? UpdatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [Column("isActive")]
    public bool? IsActive { get; set; }

    [ForeignKey("CreatedBy")]
    [InverseProperty("Trips")]
    public virtual User CreatedByNavigation { get; set; } = null!;

    [ForeignKey("DriverId")]
    [InverseProperty("Trips")]
    public virtual Driver Driver { get; set; } = null!;

    [InverseProperty("Trip")]
    public virtual ICollection<TripOrder> TripOrders { get; set; } = new List<TripOrder>();

    [ForeignKey("TripStatusId")]
    [InverseProperty("Trips")]
    public virtual LookupValue TripStatus { get; set; } = null!;
}
