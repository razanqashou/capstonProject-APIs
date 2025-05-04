using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace capAPI.Models;

[Table("DriverRate")]
public partial class DriverRate
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("DriverID")]
    public int DriverId { get; set; }

    [Column("ClientID")]
    public int ClientId { get; set; }

    [Column("OrderID")]
    public int OrderId { get; set; }

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
    [InverseProperty("DriverRates")]
    public virtual Client Client { get; set; } = null!;

    [ForeignKey("DriverId")]
    [InverseProperty("DriverRates")]
    public virtual Driver Driver { get; set; } = null!;

    [ForeignKey("OrderId")]
    [InverseProperty("DriverRates")]
    public virtual Order Order { get; set; } = null!;
}
