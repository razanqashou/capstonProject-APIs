using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace capAPI.Models;

[Table("ItemRate")]
public partial class ItemRate
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("ItemID")]
    public int ItemId { get; set; }

    [Column("ClientID")]
    public int ClientId { get; set; }

    public int Rating { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string? ReviewText { get; set; }

    [StringLength(1)]
    public string? CreatedBy { get; set; }

    [StringLength(1)]
    public string? UpdatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [Column("isActive")]
    public bool? IsActive { get; set; }

    [ForeignKey("ClientId")]
    [InverseProperty("ItemRates")]
    public virtual Client Client { get; set; } = null!;

    [ForeignKey("ItemId")]
    [InverseProperty("ItemRates")]
    public virtual Item Item { get; set; } = null!;
}
