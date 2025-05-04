using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace capAPI.Models;

public partial class Notification
{
    [Key]
    [Column("NotificationID")]
    public int NotificationId { get; set; }

    [Column("UserID")]
    public int UserId { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string Title { get; set; } = null!;

    [StringLength(500)]
    [Unicode(false)]
    public string Description { get; set; } = null!;

    [Column("NotificationTypeID")]
    public int NotificationTypeId { get; set; }

    public bool? IsRead { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    public int? CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("NotificationTypeId")]
    [InverseProperty("Notifications")]
    public virtual LookupValue NotificationType { get; set; } = null!;
}
