using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace capAPI.Models;

[Index("Phone", Name = "UQ__Users__5C7E359E2C0374A4", IsUnique = true)]
[Index("Email", Name = "UQ__Users__A9D1053479FD0A7B", IsUnique = true)]
public partial class User
{
    [Key]
    [Column("UserID")]
    public int UserId { get; set; }

    public bool? IsLoggedIn { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string Email { get; set; } = null!;

    [StringLength(10)]
    [Unicode(false)]
    public string Phone { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string? PasswordHash { get; set; }

    [Column("RoleID")]
    public int RoleId { get; set; }

    public bool? IsActive { get; set; }

    public bool? IsVerified { get; set; }

    [Unicode(false)]
    public string? ProfileImage { get; set; }

    [Column("username")]
    [StringLength(30)]
    [Unicode(false)]
    public string? Username { get; set; }

    [Column("startjoinDate", TypeName = "datetime")]
    public DateTime? StartjoinDate { get; set; }

    [Column("endjoinDate", TypeName = "datetime")]
    public DateTime? EndjoinDate { get; set; }

    [StringLength(50)]
    public string? FirstName { get; set; }

    [StringLength(50)]
    public string? LastName { get; set; }

    [StringLength(50)]
    public string? CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [StringLength(50)]
    public string? UpdatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? LastLoginTime { get; set; }

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<ClientLocation> ClientLocationCreatedByNavigations { get; set; } = new List<ClientLocation>();

    [InverseProperty("UpdatedByNavigation")]
    public virtual ICollection<ClientLocation> ClientLocationUpdatedByNavigations { get; set; } = new List<ClientLocation>();

    [InverseProperty("User")]
    public virtual ICollection<Client> Clients { get; set; } = new List<Client>();

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<Driver> Drivers { get; set; } = new List<Driver>();

    [InverseProperty("User")]
    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<Favorite> FavoriteCreatedByNavigations { get; set; } = new List<Favorite>();

    [InverseProperty("UpdatedByNavigation")]
    public virtual ICollection<Favorite> FavoriteUpdatedByNavigations { get; set; } = new List<Favorite>();

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<OfferCategory> OfferCategoryCreatedByNavigations { get; set; } = new List<OfferCategory>();

    [InverseProperty("UpdatedByNavigation")]
    public virtual ICollection<OfferCategory> OfferCategoryUpdatedByNavigations { get; set; } = new List<OfferCategory>();

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<Offer> OfferCreatedByNavigations { get; set; } = new List<Offer>();

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<OfferItem> OfferItemCreatedByNavigations { get; set; } = new List<OfferItem>();

    [InverseProperty("UpdatedByNavigation")]
    public virtual ICollection<OfferItem> OfferItemUpdatedByNavigations { get; set; } = new List<OfferItem>();

    [InverseProperty("UpdatedByNavigation")]
    public virtual ICollection<Offer> OfferUpdatedByNavigations { get; set; } = new List<Offer>();

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<OrderHistory> OrderHistoryCreatedByNavigations { get; set; } = new List<OrderHistory>();

    [InverseProperty("UpdatedByNavigation")]
    public virtual ICollection<OrderHistory> OrderHistoryUpdatedByNavigations { get; set; } = new List<OrderHistory>();

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<PickupLocation> PickupLocationCreatedByNavigations { get; set; } = new List<PickupLocation>();

    [InverseProperty("UpdatedByNavigation")]
    public virtual ICollection<PickupLocation> PickupLocationUpdatedByNavigations { get; set; } = new List<PickupLocation>();

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<Region> RegionCreatedByNavigations { get; set; } = new List<Region>();

    [InverseProperty("UpdatedByNavigation")]
    public virtual ICollection<Region> RegionUpdatedByNavigations { get; set; } = new List<Region>();

    [ForeignKey("RoleId")]
    [InverseProperty("Users")]
    public virtual Role Role { get; set; } = null!;

    [InverseProperty("Admin")]
    public virtual ICollection<TicketAction> TicketActionAdmins { get; set; } = new List<TicketAction>();

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<TicketAction> TicketActionCreatedByNavigations { get; set; } = new List<TicketAction>();

    [InverseProperty("UpdatedByNavigation")]
    public virtual ICollection<TicketAction> TicketActionUpdatedByNavigations { get; set; } = new List<TicketAction>();

    [InverseProperty("AssignedByNavigation")]
    public virtual ICollection<TripOrder> TripOrders { get; set; } = new List<TripOrder>();

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<Trip> Trips { get; set; } = new List<Trip>();

    [InverseProperty("User")]
    public virtual ICollection<UserOtpcode> UserOtpcodes { get; set; } = new List<UserOtpcode>();
}
