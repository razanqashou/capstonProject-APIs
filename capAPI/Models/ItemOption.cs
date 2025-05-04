using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace capAPI.Models;

public partial class ItemOption
{
    [Key]
    [Column("OptionID")]
    public int OptionId { get; set; }

    [Column("ItemID")]
    public int ItemId { get; set; }

    [StringLength(100)]
    public string NameAr { get; set; } = null!;

    [StringLength(100)]
    public string NameEn { get; set; } = null!;

    [Column("OptionCategoryID")]
    public int OptionCategoryId { get; set; }

    public bool? IsRequired { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? PriceAfterDiscount { get; set; }

    public int? Quantity { get; set; }

    [StringLength(1)]
    public string? CreatedBy { get; set; }

    [StringLength(1)]
    public string? UpdatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("ItemId")]
    [InverseProperty("ItemOptions")]
    public virtual Item Item { get; set; } = null!;

    [ForeignKey("OptionCategoryId")]
    [InverseProperty("ItemOptions")]
    public virtual LookupValue OptionCategory { get; set; } = null!;
}
