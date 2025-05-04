using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace capAPI.Models;

[Table("ClientWallet")]
public partial class ClientWallet
{
    [Key]
    [Column("WalletID")]
    public int WalletId { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? WalletBalance { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? LastUpdated { get; set; }

    public bool? IsActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    public int CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [Column("clientId")]
    public int? ClientId { get; set; }

    [ForeignKey("ClientId")]
    [InverseProperty("ClientWallets")]
    public virtual Client? Client { get; set; }
}
