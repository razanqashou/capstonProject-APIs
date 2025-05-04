using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace capAPI.Models;

[Index("NameEn", Name = "UQ__Items__EE1C70AF71B7B344", IsUnique = true)]
[Index("NameAr", Name = "UQ__Items__EE1CD26CF40FA811", IsUnique = true)]
public partial class Item
{
    [Key]
    [Column("ItemID")]
    public int ItemId { get; set; }

    [StringLength(100)]
    public string NameEn { get; set; } = null!;

    [StringLength(100)]
    public string NameAr { get; set; } = null!;

    [StringLength(150)]
    public string? DescriptionEn { get; set; }

    [StringLength(150)]
    public string? DescriptionAr { get; set; }

    [Unicode(false)]
    public string Image { get; set; } = null!;

    [Column(TypeName = "decimal(10, 2)")]
    public decimal Price { get; set; }

    [Column("CategoryID")]
    public int CategoryId { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? ItemBadge { get; set; }

    public bool? IsActive { get; set; }

    [StringLength(100)]
    public string? CreatedBy { get; set; }

    [StringLength(100)]
    public string? UpdatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    public bool? IsDiscount { get; set; }

    [ForeignKey("CategoryId")]
    [InverseProperty("Items")]
    public virtual Category Category { get; set; } = null!;

    [InverseProperty("Item")]
    public virtual ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();

    [InverseProperty("Item")]
    public virtual ICollection<ItemOption> ItemOptions { get; set; } = new List<ItemOption>();

    [InverseProperty("Item")]
    public virtual ICollection<ItemRate> ItemRates { get; set; } = new List<ItemRate>();

    [InverseProperty("Item")]
    public virtual ICollection<OfferItem> OfferItems { get; set; } = new List<OfferItem>();

    [InverseProperty("Item")]
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
