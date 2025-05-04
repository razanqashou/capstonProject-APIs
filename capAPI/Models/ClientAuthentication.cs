using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace capAPI.Models;

public partial class ClientAuthentication
{
    [Key]
    [Column("AuthID")]
    public int AuthId { get; set; }

    [Column("ClientID")]
    public int ClientId { get; set; }

    public int AuthProviderType { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string AuthProviderKey { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    public int? CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("AuthProviderType")]
    [InverseProperty("ClientAuthentications")]
    public virtual LookupValue AuthProviderTypeNavigation { get; set; } = null!;

    [ForeignKey("ClientId")]
    [InverseProperty("ClientAuthentications")]
    public virtual Client Client { get; set; } = null!;
}
