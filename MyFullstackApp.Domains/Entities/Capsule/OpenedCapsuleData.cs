using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyFullstackApp.Domains.Entities.Capsule;

public class OpenedCapsuleData
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public int UserId { get; set; }

    public int CapsuleId { get; set; }

    public DateTime OpenedAtUtc { get; set; }

    [StringLength(200)]
    public string? OpenedFrom { get; set; }
}
