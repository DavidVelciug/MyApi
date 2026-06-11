namespace MyFullstackApp.Domains.Models.Capsule;

public class OpenedCapsuleDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int CapsuleId { get; set; }
    public DateTime OpenedAtUtc { get; set; }
    public string? OpenedFrom { get; set; }
}

public class RecordOpenedCapsuleDto
{
    public int UserId { get; set; }
    public int CapsuleId { get; set; }
    public string? OpenedFrom { get; set; }
}
