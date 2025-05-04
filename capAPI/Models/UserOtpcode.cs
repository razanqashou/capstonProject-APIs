using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace capAPI.Models;

[Table("UserOTPCodes")]
public partial class UserOtpcode
{
    [Key]
    [Column("OTPID")]
    public int Otpid { get; set; }

    [Column("UserID")]
    public int UserId { get; set; }

    [Column("OTPCode")]
    [StringLength(10)]
    public string? Otpcode { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime CreatedAt { get; set; }

    [StringLength(20)]
    public string? CreatedBy { get; set; }

    [Column("updatedBy")]
    [StringLength(20)]
    public string? UpdatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ExpiresAt { get; set; } 

    public bool IsUsed { get; set; }

    [Column("isActive")]
    public bool IsActive { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("UserOtpcodes")]
    public virtual User User { get; set; } = null!;

    


}
