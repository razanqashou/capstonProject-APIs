using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace capAPI.Models;

public partial class Offer
{
    [Key]
    [Column("OfferID")]
    public int OfferId { get; set; }

    [StringLength(100)]
    public string TitleEn { get; set; } = null!;

    public string TitleAr { get; set; } = null!;

    public string? DescriptionEn { get; set; }

    public string? DescriptionAr { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime StartDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime EndDate { get; set; }

    public double? LimitAmount { get; set; }

    public int? LimitPersons { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Code { get; set; }

    public int? DiscountPercent { get; set; }

    [Column("OfferStatusID")]
    public int OfferStatusId { get; set; }

    [Unicode(false)]
    public string? Image { get; set; }

    public bool? IsActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    public int? CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("CreatedBy")]
    [InverseProperty("OfferCreatedByNavigations")]
    public virtual User? CreatedByNavigation { get; set; }

    [InverseProperty("Offer")]
    public virtual ICollection<OfferCategory> OfferCategories { get; set; } = new List<OfferCategory>();

    [InverseProperty("Offer")]
    public virtual ICollection<OfferItem> OfferItems { get; set; } = new List<OfferItem>();

    [ForeignKey("OfferStatusId")]
    [InverseProperty("Offers")]
    public virtual LookupValue OfferStatus { get; set; } = null!;

    [ForeignKey("UpdatedBy")]
    [InverseProperty("OfferUpdatedByNavigations")]
    public virtual User? UpdatedByNavigation { get; set; }
}
