using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace capAPI.Models;

public partial class OfferCategory
{
    [Key]
    [Column("OfferCategoryID")]
    public int OfferCategoryId { get; set; }

    [Column("OfferID")]
    public int OfferId { get; set; }

    [Column("CategoryID")]
    public int CategoryId { get; set; }

    public int? CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [ForeignKey("CategoryId")]
    [InverseProperty("OfferCategories")]
    public virtual Category Category { get; set; } = null!;

    [ForeignKey("CreatedBy")]
    [InverseProperty("OfferCategoryCreatedByNavigations")]
    public virtual User? CreatedByNavigation { get; set; }

    [ForeignKey("OfferId")]
    [InverseProperty("OfferCategories")]
    public virtual Offer Offer { get; set; } = null!;

    [ForeignKey("UpdatedBy")]
    [InverseProperty("OfferCategoryUpdatedByNavigations")]
    public virtual User? UpdatedByNavigation { get; set; }
}
