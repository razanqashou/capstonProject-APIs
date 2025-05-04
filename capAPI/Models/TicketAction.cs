using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace capAPI.Models;

public partial class TicketAction
{
    [Key]
    [Column("ActionID")]
    public int ActionId { get; set; }

    [Column("TicketID")]
    public int TicketId { get; set; }

    [Column("AdminID")]
    public int AdminId { get; set; }

    [Column(TypeName = "text")]
    public string? Response { get; set; }

    [Column("ActionTypeID")]
    public int? ActionTypeId { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? RefundAmount { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? RefundExpirationDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    public int? CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("ActionTypeId")]
    [InverseProperty("TicketActions")]
    public virtual LookupValue? ActionType { get; set; }

    [ForeignKey("AdminId")]
    [InverseProperty("TicketActionAdmins")]
    public virtual User Admin { get; set; } = null!;

    [ForeignKey("CreatedBy")]
    [InverseProperty("TicketActionCreatedByNavigations")]
    public virtual User? CreatedByNavigation { get; set; }

    [ForeignKey("TicketId")]
    [InverseProperty("TicketActions")]
    public virtual Ticket Ticket { get; set; } = null!;

    [ForeignKey("UpdatedBy")]
    [InverseProperty("TicketActionUpdatedByNavigations")]
    public virtual User? UpdatedByNavigation { get; set; }
}
