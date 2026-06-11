using MyFullstackApp.Domains.Models.Base;
using MyFullstackApp.Domains.Models.Reaction;

namespace MyFullstackApp.BusinessLogic.Interface;

public interface IReaction
{
    ReactionCountsDto GetCapsuleReactionCountsAction(int capsuleId);
    ReactionCountsDto GetProductReactionCountsAction(int productId);
    string? GetUserCapsuleReactionAction(int userId, int capsuleId);
    string? GetUserProductReactionAction(int userId, int productId);
    ResponceMsg ToggleCapsuleReactionAction(int userId, int capsuleId, string type);
    ResponceMsg ToggleProductReactionAction(int userId, int productId, string type);
}
