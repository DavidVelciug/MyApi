using Microsoft.AspNetCore.Mvc;
using MyApi.Filters;
using MyFullstackApp.BusinessLogic;
using MyFullstackApp.BusinessLogic.Interface;
using MyFullstackApp.Domains.Models.Capsule;

namespace MyApi.Controller;

[Route("api/openedcapsule")]
[ApiController]
[RoleAccess(AppRoles.User, AppRoles.Moderator, AppRoles.Admin)]
public class OpenedCapsuleController : ControllerBase
{
    private readonly ITimeCapsule _capsules;

    public OpenedCapsuleController(BusinessLogic businessLogic)
    {
        _capsules = businessLogic.GetTimeCapsuleActions();
    }

    [HttpGet("forUser")]
    public IActionResult ForUser(int userId)
    {
        return Ok(_capsules.GetOpenedCapsulesForUserAction(userId));
    }

    [HttpGet("ids")]
    public IActionResult GetOpenedIds(int userId)
    {
        return Ok(_capsules.GetOpenedCapsuleIdsForUserAction(userId));
    }

    [HttpPost]
    public IActionResult Record([FromBody] RecordOpenedCapsuleDto dto)
    {
        return Ok(_capsules.RecordOpenedCapsuleAction(dto.UserId, dto.CapsuleId, dto.OpenedFrom));
    }
}
