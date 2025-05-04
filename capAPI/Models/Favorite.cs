using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace capAPI.Models;

[Index("ClientId", "ItemId", Name = "UQ_Client_Item", IsUnique = true)]
public partial class Favorite
{
    [Key]
    [Column("FavoriteID")]
    public int FavoriteId { get; set; }

    [Column("ClientID")]
    public int ClientId { get; set; }

    [Column("ItemID")]
    public int ItemId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    public bool? IsActive { get; set; }

    public int? CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("ClientId")]
    [InverseProperty("Favorites")]
    public virtual Client Client { get; set; } = null!;

    [ForeignKey("CreatedBy")]
    [InverseProperty("FavoriteCreatedByNavigations")]
    public virtual User? CreatedByNavigation { get; set; }

    [ForeignKey("ItemId")]
    [InverseProperty("Favorites")]
    public virtual Item Item { get; set; } = null!;

    [ForeignKey("UpdatedBy")]
    [InverseProperty("FavoriteUpdatedByNavigations")]
    public virtual User? UpdatedByNavigation { get; set; }
}
