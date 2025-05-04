using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace capAPI.Models;

[Table("OrderHistory")]
public partial class OrderHistory
{
    [Key]
    [Column("HistoryID")]
    public int HistoryId { get; set; }

    [Column("OrderID")]
    public int OrderId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    public bool? IsActive { get; set; }

    public int? CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("CreatedBy")]
    [InverseProperty("OrderHistoryCreatedByNavigations")]
    public virtual User? CreatedByNavigation { get; set; }

    [ForeignKey("OrderId")]
    [InverseProperty("OrderHistories")]
    public virtual Order Order { get; set; } = null!;

    [ForeignKey("UpdatedBy")]
    [InverseProperty("OrderHistoryUpdatedByNavigations")]
    public virtual User? UpdatedByNavigation { get; set; }
}
