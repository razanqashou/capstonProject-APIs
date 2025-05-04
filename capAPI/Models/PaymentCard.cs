using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace capAPI.Models;

public partial class PaymentCard
{
    [Key]
    [Column("CardID")]
    public int CardId { get; set; }

    [Column("ClientID")]
    public int ClientId { get; set; }

    [Column("PaymentMethodID")]
    public int PaymentMethodId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string HolderName { get; set; } = null!;

    [StringLength(4)]
    [Unicode(false)]
    public string Last4Digits { get; set; } = null!;

    [StringLength(19)]
    [Unicode(false)]
    public string MaskedCard { get; set; } = null!;

    [StringLength(7)]
    [Unicode(false)]
    public string ExpireDate { get; set; } = null!;

    [Column("CVCCodeEncrypted")]
    [StringLength(200)]
    [Unicode(false)]
    public string? CvccodeEncrypted { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? OptionalTitle { get; set; }

    public bool? IsDefault { get; set; }

    public bool? IsActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    public int CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("ClientId")]
    [InverseProperty("PaymentCards")]
    public virtual Client Client { get; set; } = null!;

    [ForeignKey("PaymentMethodId")]
    [InverseProperty("PaymentCards")]
    public virtual LookupValue PaymentMethod { get; set; } = null!;
}
