using Microsoft.AspNetCore.Mvc;
using MyApi.Filters;
using MyFullstackApp.BusinessLogic;
using MyFullstackApp.BusinessLogic.Interface;

namespace MyApi.Controller;

[Route("api/admin/stats")]
[ApiController]
[RoleAccess(AppRoles.Admin)]
public class AdminStatsController : ControllerBase
{
    private readonly IAdminAnalytics _analytics;

    public AdminStatsController(BusinessLogic businessLogic)
    {
        _analytics = businessLogic.GetAdminAnalyticsActions();
    }

    [HttpGet("getAnalytics")]
    public IActionResult GetAnalytics()
    {
        return Ok(_analytics.GetAdminStatsAction());
    }
}
