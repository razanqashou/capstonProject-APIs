using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace capAPI.Models;

public partial class TripOrder
{
    [Key]
    [Column("TripOrderID")]
    public int TripOrderId { get; set; }

    [Column("TripID")]
    public int TripId { get; set; }

    [Column("OrderID")]
    public int OrderId { get; set; }

    [Column("TripOrderStatusID")]
    public int TripOrderStatusId { get; set; }

    public int? SequenceOrder { get; set; }

    public int AssignedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? AssignedAt { get; set; }

    [StringLength(1)]
    public string? UpdatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [Column("isActive")]
    public bool? IsActive { get; set; }

    [ForeignKey("AssignedBy")]
    [InverseProperty("TripOrders")]
    public virtual User AssignedByNavigation { get; set; } = null!;

    [ForeignKey("OrderId")]
    [InverseProperty("TripOrders")]
    public virtual Order Order { get; set; } = null!;

    [ForeignKey("TripId")]
    [InverseProperty("TripOrders")]
    public virtual Trip Trip { get; set; } = null!;

    [ForeignKey("TripOrderStatusId")]
    [InverseProperty("TripOrders")]
    public virtual LookupValue TripOrderStatus { get; set; } = null!;
}
