using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace capAPI.Models;

[Table("LookupValue")]
public partial class LookupValue
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("value")]
    [StringLength(50)]
    public string Value { get; set; } = null!;

    [Column("lookupItemId")]
    public int? LookupItemId { get; set; }

    [InverseProperty("AuthProviderTypeNavigation")]
    public virtual ICollection<ClientAuthentication> ClientAuthentications { get; set; } = new List<ClientAuthentication>();

    [InverseProperty("ProvinceLookup")]
    public virtual ICollection<ClientLocation> ClientLocations { get; set; } = new List<ClientLocation>();

    [InverseProperty("VehicleTypeNavigation")]
    public virtual ICollection<Driver> Drivers { get; set; } = new List<Driver>();

    [InverseProperty("EmployeeTypeNavigation")]
    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    [InverseProperty("OptionCategory")]
    public virtual ICollection<ItemOption> ItemOptions { get; set; } = new List<ItemOption>();

    [ForeignKey("LookupItemId")]
    [InverseProperty("LookupValues")]
    public virtual LookupItem? LookupItem { get; set; }

    [InverseProperty("NotificationType")]
    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    [InverseProperty("OfferStatus")]
    public virtual ICollection<Offer> Offers { get; set; } = new List<Offer>();

    [InverseProperty("PaymentMethod")]
    public virtual ICollection<Order> OrderPaymentMethods { get; set; } = new List<Order>();

    [InverseProperty("PaymentStatus")]
    public virtual ICollection<Order> OrderPaymentStatuses { get; set; } = new List<Order>();

    [InverseProperty("Status")]
    public virtual ICollection<Order> OrderStatuses { get; set; } = new List<Order>();

    [InverseProperty("PaymentMethod")]
    public virtual ICollection<PaymentCard> PaymentCards { get; set; } = new List<PaymentCard>();

    [InverseProperty("ProvinceLookup")]
    public virtual ICollection<PickupLocation> PickupLocations { get; set; } = new List<PickupLocation>();

    [InverseProperty("ProvinceLookup")]
    public virtual ICollection<Region> Regions { get; set; } = new List<Region>();

    [InverseProperty("ActionType")]
    public virtual ICollection<TicketAction> TicketActions { get; set; } = new List<TicketAction>();

    [InverseProperty("StatusIssue")]
    public virtual ICollection<Ticket> TicketStatusIssues { get; set; } = new List<Ticket>();

    [InverseProperty("StatusSuggestion")]
    public virtual ICollection<Ticket> TicketStatusSuggestions { get; set; } = new List<Ticket>();

    [InverseProperty("TicketType")]
    public virtual ICollection<Ticket> TicketTicketTypes { get; set; } = new List<Ticket>();

    [InverseProperty("PaymentMethod")]
    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

    [InverseProperty("TripOrderStatus")]
    public virtual ICollection<TripOrder> TripOrders { get; set; } = new List<TripOrder>();

    [InverseProperty("TripStatus")]
    public virtual ICollection<Trip> Trips { get; set; } = new List<Trip>();

    [InverseProperty("PaymentMethod")]
    public virtual ICollection<WalletTransaction> WalletTransactions { get; set; } = new List<WalletTransaction>();
}
