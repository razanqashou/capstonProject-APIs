using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace capAPI.Models;

public partial class Category
{
    [Key]
    [Column("CategoryID")]
    public int CategoryId { get; set; }

    public string NameAr { get; set; } = null!;

    public string NameEn { get; set; } = null!;

    [Unicode(false)]
    public string Image { get; set; } = null!;

    public int? ItemCount { get; set; }

    public bool? IsActive { get; set; }

    [StringLength(1)]
    public string? CreatedBy { get; set; }

    [StringLength(1)]
    public string? UpdatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [InverseProperty("Category")]
    public virtual ICollection<Item> Items { get; set; } = new List<Item>();

    [InverseProperty("Category")]
    public virtual ICollection<OfferCategory> OfferCategories { get; set; } = new List<OfferCategory>();
}
