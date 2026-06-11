using Microsoft.AspNetCore.Mvc;
using MyApi.Filters;
using MyFullstackApp.BusinessLogic;
using MyFullstackApp.BusinessLogic.Interface;
using MyFullstackApp.Domains.Models.Reaction;

namespace MyApi.Controller;

[Route("api/reaction")]
[ApiController]
[RoleAccess(AppRoles.Guest, AppRoles.User, AppRoles.Moderator, AppRoles.Admin)]
public class ReactionController : ControllerBase
{
    private readonly IReaction _reactions;

    public ReactionController(BusinessLogic businessLogic)
    {
        _reactions = businessLogic.GetReactionActions();
    }

    [HttpGet("capsule/counts")]
    public IActionResult GetCapsuleCounts(int capsuleId)
    {
        return Ok(_reactions.GetCapsuleReactionCountsAction(capsuleId));
    }

    [HttpGet("product/counts")]
    public IActionResult GetProductCounts(int productId)
    {
        return Ok(_reactions.GetProductReactionCountsAction(productId));
    }

    [HttpGet("capsule/user")]
    public IActionResult GetUserCapsuleReaction(int userId, int capsuleId)
    {
        var type = _reactions.GetUserCapsuleReactionAction(userId, capsuleId);
        return Ok(new { reaction = type });
    }

    [HttpGet("product/user")]
    public IActionResult GetUserProductReaction(int userId, int productId)
    {
        var type = _reactions.GetUserProductReactionAction(userId, productId);
        return Ok(new { reaction = type });
    }

    [HttpPost("capsule/toggle")]
    [RoleAccess(AppRoles.User, AppRoles.Moderator, AppRoles.Admin)]
    public IActionResult ToggleCapsule([FromBody] UserReactionDto dto)
    {
        if (dto.CapsuleId == null)
            return BadRequest(new { isSuccess = false, message = "CapsuleId is required." });
        return Ok(_reactions.ToggleCapsuleReactionAction(dto.UserId, dto.CapsuleId.Value, dto.Type));
    }

    [HttpPost("product/toggle")]
    [RoleAccess(AppRoles.User, AppRoles.Moderator, AppRoles.Admin)]
    public IActionResult ToggleProduct([FromBody] UserReactionDto dto)
    {
        if (dto.ProductId == null)
            return BadRequest(new { isSuccess = false, message = "ProductId is required." });
        return Ok(_reactions.ToggleProductReactionAction(dto.UserId, dto.ProductId.Value, dto.Type));
    }
}
