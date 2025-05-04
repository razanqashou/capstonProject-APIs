using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace capAPI.Models;

public partial class Region
{
    [Key]
    [Column("RegionID")]
    public int RegionId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [Column("ProvinceLookupID")]
    public int ProvinceLookupId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    public int? CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [InverseProperty("Region")]
    public virtual ICollection<ClientLocation> ClientLocations { get; set; } = new List<ClientLocation>();

    [ForeignKey("CreatedBy")]
    [InverseProperty("RegionCreatedByNavigations")]
    public virtual User? CreatedByNavigation { get; set; }

    [InverseProperty("Region")]
    public virtual ICollection<PickupLocation> PickupLocations { get; set; } = new List<PickupLocation>();

    [ForeignKey("ProvinceLookupId")]
    [InverseProperty("Regions")]
    public virtual LookupValue ProvinceLookup { get; set; } = null!;

    [ForeignKey("UpdatedBy")]
    [InverseProperty("RegionUpdatedByNavigations")]
    public virtual User? UpdatedByNavigation { get; set; }
}
