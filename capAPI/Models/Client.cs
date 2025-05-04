using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace capAPI.Models;

public partial class Client
{
    [Key]
    [Column("ClientID")]
    public int ClientId { get; set; }

    public DateOnly BirthDate { get; set; }

    public int? OrderCount { get; set; }

    public int? CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    public int? UpdatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [Column("isActive")]
    public bool? IsActive { get; set; }

    [Column("userId")]
    public int? UserId { get; set; }

    [InverseProperty("Client")]
    public virtual ICollection<ClientAuthentication> ClientAuthentications { get; set; } = new List<ClientAuthentication>();

    [InverseProperty("Client")]
    public virtual ICollection<ClientLocation> ClientLocations { get; set; } = new List<ClientLocation>();

    [InverseProperty("Client")]
    public virtual ICollection<ClientWallet> ClientWallets { get; set; } = new List<ClientWallet>();

    [InverseProperty("Client")]
    public virtual ICollection<DriverRate> DriverRates { get; set; } = new List<DriverRate>();

    [InverseProperty("Client")]
    public virtual ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();

    [InverseProperty("Client")]
    public virtual ICollection<ItemRate> ItemRates { get; set; } = new List<ItemRate>();

    [InverseProperty("Client")]
    public virtual ICollection<OrderRate> OrderRates { get; set; } = new List<OrderRate>();

    [InverseProperty("Client")]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    [InverseProperty("Client")]
    public virtual ICollection<PaymentCard> PaymentCards { get; set; } = new List<PaymentCard>();

    [InverseProperty("Client")]
    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

    [ForeignKey("UserId")]
    [InverseProperty("Clients")]
    public virtual User? User { get; set; }

    [InverseProperty("Client")]
    public virtual ICollection<WalletTransaction> WalletTransactions { get; set; } = new List<WalletTransaction>();
}
