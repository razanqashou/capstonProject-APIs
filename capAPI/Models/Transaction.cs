using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace capAPI.Models;

public partial class Transaction
{
    [Key]
    [Column("TransactionID")]
    public int TransactionId { get; set; }

    [Column("OrderID")]
    public int OrderId { get; set; }

    [Column("PaymentMethodID")]
    public int PaymentMethodId { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal PaymentAmount { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? TransactionDate { get; set; }

    public bool IsSuccessful { get; set; }

    public bool? IsActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    public int CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("OrderId")]
    [InverseProperty("Transactions")]
    public virtual Order Order { get; set; } = null!;

    [ForeignKey("PaymentMethodId")]
    [InverseProperty("Transactions")]
    public virtual LookupValue PaymentMethod { get; set; } = null!;
}
