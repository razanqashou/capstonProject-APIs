using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace capAPI.Models;

public partial class ClientLocation
{
    [Key]
    [Column("LocationID")]
    public int LocationId { get; set; }

    [Column("ClientID")]
    public int ClientId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Title { get; set; }

    [Column("ProvinceLookupID")]
    public int ProvinceLookupId { get; set; }

    [Column("RegionID")]
    public int RegionId { get; set; }

    [StringLength(250)]
    [Unicode(false)]
    public string? Description { get; set; }

    public double? Latitude { get; set; }

    public double? Longitude { get; set; }

    [Unicode(false)]
    public string? MapImageUrl { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    public int? CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("ClientId")]
    [InverseProperty("ClientLocations")]
    public virtual Client Client { get; set; } = null!;

    [ForeignKey("CreatedBy")]
    [InverseProperty("ClientLocationCreatedByNavigations")]
    public virtual User? CreatedByNavigation { get; set; }

    [ForeignKey("ProvinceLookupId")]
    [InverseProperty("ClientLocations")]
    public virtual LookupValue ProvinceLookup { get; set; } = null!;

    [ForeignKey("RegionId")]
    [InverseProperty("ClientLocations")]
    public virtual Region Region { get; set; } = null!;

    [ForeignKey("UpdatedBy")]
    [InverseProperty("ClientLocationUpdatedByNavigations")]
    public virtual User? UpdatedByNavigation { get; set; }
}
