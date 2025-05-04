using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace capAPI.Models;

public partial class Order
{
    [Key]
    [Column("OrderID")]
    public int OrderId { get; set; }

    [Column("ClientID")]
    public int ClientId { get; set; }

    [Column("DriverID")]
    public int? DriverId { get; set; }

    [Column("TripID")]
    public int? TripId { get; set; }

    [Column("StatusID")]
    public int StatusId { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal SubTotal { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal DeliveryCharge { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal TotalPrice { get; set; }

    [StringLength(4)]
    [Unicode(false)]
    public string SecretCode { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime? SecretCodeExpiry { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DeliveryDate { get; set; }

    [Column("PickupLocationID")]
    public int PickupLocationId { get; set; }

    [Column("DeliveryLocationID")]
    public int DeliveryLocationId { get; set; }

    [StringLength(1)]
    public string? CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [StringLength(1)]
    public string? UpdatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [Column("PaymentStatusID")]
    public int? PaymentStatusId { get; set; }

    [Column("PaymentMethodID")]
    public int? PaymentMethodId { get; set; }

    [ForeignKey("ClientId")]
    [InverseProperty("Orders")]
    public virtual Client Client { get; set; } = null!;

    [ForeignKey("DriverId")]
    [InverseProperty("Orders")]
    public virtual Driver? Driver { get; set; }

    [InverseProperty("Order")]
    public virtual ICollection<DriverRate> DriverRates { get; set; } = new List<DriverRate>();

    [InverseProperty("Order")]
    public virtual ICollection<OrderHistory> OrderHistories { get; set; } = new List<OrderHistory>();

    [InverseProperty("Order")]
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    [InverseProperty("Order")]
    public virtual ICollection<OrderRate> OrderRates { get; set; } = new List<OrderRate>();

    [ForeignKey("PaymentMethodId")]
    [InverseProperty("OrderPaymentMethods")]
    public virtual LookupValue? PaymentMethod { get; set; }

    [ForeignKey("PaymentStatusId")]
    [InverseProperty("OrderPaymentStatuses")]
    public virtual LookupValue? PaymentStatus { get; set; }

    [ForeignKey("PickupLocationId")]
    [InverseProperty("Orders")]
    public virtual PickupLocation PickupLocation { get; set; } = null!;

    [ForeignKey("StatusId")]
    [InverseProperty("OrderStatuses")]
    public virtual LookupValue Status { get; set; } = null!;

    [InverseProperty("Order")]
    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

    [InverseProperty("Order")]
    public virtual ICollection<TripOrder> TripOrders { get; set; } = new List<TripOrder>();
}
