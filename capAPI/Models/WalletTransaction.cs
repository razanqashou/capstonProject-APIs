using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace capAPI.Models;

public partial class WalletTransaction
{
    [Key]
    [Column("TransactionID")]
    public int TransactionId { get; set; }

    [Column("ClientID")]
    public int ClientId { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal Amount { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? TransactionDate { get; set; }

    [Column("PaymentMethodID")]
    public int PaymentMethodId { get; set; }

    public bool IsSuccessful { get; set; }

    public bool? IsActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    public int CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("ClientId")]
    [InverseProperty("WalletTransactions")]
    public virtual Client Client { get; set; } = null!;

    [ForeignKey("PaymentMethodId")]
    [InverseProperty("WalletTransactions")]
    public virtual LookupValue PaymentMethod { get; set; } = null!;
}
