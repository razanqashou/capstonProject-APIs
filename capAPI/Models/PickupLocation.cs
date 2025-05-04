using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace capAPI.Models;

public partial class PickupLocation
{
    [Key]
    [Column("PickupLocationID")]
    public int PickupLocationId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [Column("ProvinceLookupID")]
    public int ProvinceLookupId { get; set; }

    [Column("RegionID")]
    public int RegionId { get; set; }

    [Column(TypeName = "text")]
    public string? Description { get; set; }

    public double? Latitude { get; set; }

    public double? Longitude { get; set; }

    public bool? IsActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    public int? CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("CreatedBy")]
    [InverseProperty("PickupLocationCreatedByNavigations")]
    public virtual User? CreatedByNavigation { get; set; }

    [InverseProperty("PickupLocation")]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    [ForeignKey("ProvinceLookupId")]
    [InverseProperty("PickupLocations")]
    public virtual LookupValue ProvinceLookup { get; set; } = null!;

    [ForeignKey("RegionId")]
    [InverseProperty("PickupLocations")]
    public virtual Region Region { get; set; } = null!;

    [ForeignKey("UpdatedBy")]
    [InverseProperty("PickupLocationUpdatedByNavigations")]
    public virtual User? UpdatedByNavigation { get; set; }
}
