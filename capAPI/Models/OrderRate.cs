using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace capAPI.Models;

[Table("OrderRate")]
public partial class OrderRate
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("OrderID")]
    public int OrderId { get; set; }

    [Column("ClientID")]
    public int ClientId { get; set; }

    public double Rating { get; set; }

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
    [InverseProperty("OrderRates")]
    public virtual Client Client { get; set; } = null!;

    [ForeignKey("OrderId")]
    [InverseProperty("OrderRates")]
    public virtual Order Order { get; set; } = null!;
}
