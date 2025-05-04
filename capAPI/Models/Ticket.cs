using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace capAPI.Models;

[Table("Ticket")]
public partial class Ticket
{
    [Key]
    [Column("TicketID")]
    public int TicketId { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? Title { get; set; }

    [Column(TypeName = "text")]
    public string Description { get; set; } = null!;

    [Column("TicketTypeID")]
    public int TicketTypeId { get; set; }

    [Column("StatusIssueID")]
    public int? StatusIssueId { get; set; }

    [Column("StatusSuggestionID")]
    public int? StatusSuggestionId { get; set; }

    [Column("ClientID")]
    public int ClientId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime StartDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime EndDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    public int? CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("ClientId")]
    [InverseProperty("Tickets")]
    public virtual Client Client { get; set; } = null!;

    [ForeignKey("StatusIssueId")]
    [InverseProperty("TicketStatusIssues")]
    public virtual LookupValue? StatusIssue { get; set; }

    [ForeignKey("StatusSuggestionId")]
    [InverseProperty("TicketStatusSuggestions")]
    public virtual LookupValue? StatusSuggestion { get; set; }

    [InverseProperty("Ticket")]
    public virtual ICollection<TicketAction> TicketActions { get; set; } = new List<TicketAction>();

    [ForeignKey("TicketTypeId")]
    [InverseProperty("TicketTicketTypes")]
    public virtual LookupValue TicketType { get; set; } = null!;
}
