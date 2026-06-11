using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyFullstackApp.Domains.Entities.Reaction;

public class ReactionData
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public int UserId { get; set; }

    public int? CapsuleId { get; set; }

    public int? ProductId { get; set; }

    [Required]
    [StringLength(10)]
    public string Type { get; set; } = "like";

    public DateTime CreatedAtUtc { get; set; }
}
