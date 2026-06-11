using MyFullstackApp.BusinessLogic.Core.Reactions;
using MyFullstackApp.BusinessLogic.Interface;
using MyFullstackApp.Domains.Models.Base;
using MyFullstackApp.Domains.Models.Reaction;

namespace MyFullstackApp.BusinessLogic.Functions.Reactions;

public class ReactionFlow : ReactionAction, IReaction
{
    public ReactionCountsDto GetCapsuleReactionCountsAction(int capsuleId) =>
        ExecuteGetCapsuleReactionCountsAction(capsuleId);

    public ReactionCountsDto GetProductReactionCountsAction(int productId) =>
        ExecuteGetProductReactionCountsAction(productId);

    public string? GetUserCapsuleReactionAction(int userId, int capsuleId) =>
        ExecuteGetUserCapsuleReactionAction(userId, capsuleId);

    public string? GetUserProductReactionAction(int userId, int productId) =>
        ExecuteGetUserProductReactionAction(userId, productId);

    public ResponceMsg ToggleCapsuleReactionAction(int userId, int capsuleId, string type) =>
        ExecuteToggleCapsuleReactionAction(userId, capsuleId, type);

    public ResponceMsg ToggleProductReactionAction(int userId, int productId, string type) =>
        ExecuteToggleProductReactionAction(userId, productId, type);
}
