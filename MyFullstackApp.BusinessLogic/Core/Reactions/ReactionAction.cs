using MyFullstackApp.DataAccess.Context;
using MyFullstackApp.Domains.Entities.Reaction;
using MyFullstackApp.Domains.Models.Base;
using MyFullstackApp.Domains.Models.Reaction;

namespace MyFullstackApp.BusinessLogic.Core.Reactions;

public class ReactionAction
{
    protected ReactionCountsDto ExecuteGetCapsuleReactionCountsAction(int capsuleId)
    {
        using var db = new AppDbContext();
        var likes = db.Reactions.Count(r => r.CapsuleId == capsuleId && r.Type == "like");
        var dislikes = db.Reactions.Count(r => r.CapsuleId == capsuleId && r.Type == "dislike");
        return new ReactionCountsDto { Likes = likes, Dislikes = dislikes };
    }

    protected ReactionCountsDto ExecuteGetProductReactionCountsAction(int productId)
    {
        using var db = new AppDbContext();
        var likes = db.Reactions.Count(r => r.ProductId == productId && r.Type == "like");
        var dislikes = db.Reactions.Count(r => r.ProductId == productId && r.Type == "dislike");
        return new ReactionCountsDto { Likes = likes, Dislikes = dislikes };
    }

    protected string? ExecuteGetUserCapsuleReactionAction(int userId, int capsuleId)
    {
        using var db = new AppDbContext();
        return db.Reactions
            .Where(r => r.UserId == userId && r.CapsuleId == capsuleId)
            .Select(r => r.Type)
            .FirstOrDefault();
    }

    protected string? ExecuteGetUserProductReactionAction(int userId, int productId)
    {
        using var db = new AppDbContext();
        return db.Reactions
            .Where(r => r.UserId == userId && r.ProductId == productId)
            .Select(r => r.Type)
            .FirstOrDefault();
    }

    protected ResponceMsg ExecuteToggleCapsuleReactionAction(int userId, int capsuleId, string type)
    {
        using var db = new AppDbContext();
        if (type != "like" && type != "dislike")
        {
            return new ResponceMsg { IsSuccess = false, Message = "Invalid reaction type." };
        }

        if (!db.TimeCapsules.Any(c => c.Id == capsuleId))
        {
            return new ResponceMsg { IsSuccess = false, Message = "Capsule not found." };
        }

        var existing = db.Reactions.FirstOrDefault(r => r.UserId == userId && r.CapsuleId == capsuleId);
        if (existing != null)
        {
            if (existing.Type == type)
            {
                db.Reactions.Remove(existing);
            }
            else
            {
                existing.Type = type;
                existing.CreatedAtUtc = DateTime.UtcNow;
            }
        }
        else
        {
            db.Reactions.Add(new ReactionData
            {
                UserId = userId,
                CapsuleId = capsuleId,
                Type = type,
                CreatedAtUtc = DateTime.UtcNow
            });
        }

        db.SaveChanges();
        return new ResponceMsg { IsSuccess = true, Message = "Reaction updated." };
    }

    protected ResponceMsg ExecuteToggleProductReactionAction(int userId, int productId, string type)
    {
        using var db = new AppDbContext();
        if (type != "like" && type != "dislike")
        {
            return new ResponceMsg { IsSuccess = false, Message = "Invalid reaction type." };
        }

        if (!db.Products.Any(p => p.Id == productId))
        {
            return new ResponceMsg { IsSuccess = false, Message = "Product not found." };
        }

        var existing = db.Reactions.FirstOrDefault(r => r.UserId == userId && r.ProductId == productId);
        if (existing != null)
        {
            if (existing.Type == type)
            {
                db.Reactions.Remove(existing);
            }
            else
            {
                existing.Type = type;
                existing.CreatedAtUtc = DateTime.UtcNow;
            }
        }
        else
        {
            db.Reactions.Add(new ReactionData
            {
                UserId = userId,
                ProductId = productId,
                Type = type,
                CreatedAtUtc = DateTime.UtcNow
            });
        }

        db.SaveChanges();
        return new ResponceMsg { IsSuccess = true, Message = "Reaction updated." };
    }
}
