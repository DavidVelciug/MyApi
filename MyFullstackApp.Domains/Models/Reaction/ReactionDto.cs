namespace MyFullstackApp.Domains.Models.Reaction;

public class ReactionDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int? CapsuleId { get; set; }
    public int? ProductId { get; set; }
    public string Type { get; set; } = "like";
    public DateTime CreatedAtUtc { get; set; }
}

public class ReactionCountsDto
{
    public int Likes { get; set; }
    public int Dislikes { get; set; }
}

public class UserReactionDto
{
    public int UserId { get; set; }
    public int? CapsuleId { get; set; }
    public int? ProductId { get; set; }
    public string Type { get; set; } = "like";
}
