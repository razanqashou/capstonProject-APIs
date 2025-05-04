using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace capAPI.Models;

public partial class OfferItem
{
    [Key]
    [Column("OfferItemID")]
    public int OfferItemId { get; set; }

    [Column("OfferID")]
    public int OfferId { get; set; }

    [Column("ItemID")]
    public int ItemId { get; set; }

    public int? CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [ForeignKey("CreatedBy")]
    [InverseProperty("OfferItemCreatedByNavigations")]
    public virtual User? CreatedByNavigation { get; set; }

    [ForeignKey("ItemId")]
    [InverseProperty("OfferItems")]
    public virtual Item Item { get; set; } = null!;

    [ForeignKey("OfferId")]
    [InverseProperty("OfferItems")]
    public virtual Offer Offer { get; set; } = null!;

    [ForeignKey("UpdatedBy")]
    [InverseProperty("OfferItemUpdatedByNavigations")]
    public virtual User? UpdatedByNavigation { get; set; }
}
