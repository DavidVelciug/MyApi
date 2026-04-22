using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MyApi.Filters;

public static class AppRoles
{
    public const string Guest = "guest";
    public const string User = "user";
    public const string Moderator = "moderator";
    public const string Admin = "admin";

    public static readonly string[] All = [Guest, User, Moderator, Admin];
}

/// <summary>
/// Фильтр лабораторной работы: должен выполняться до остальных фильтров доступа.
/// Разрешает действие только ролям администратора или модератора.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public sealed class AdminModAttribute : ActionFilterAttribute, IOrderedFilter
{
    public new int Order => -200;

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var role = RoleResolver.GetRole(context);
        if (role is AppRoles.Admin or AppRoles.Moderator)
        {
            base.OnActionExecuting(context);
            return;
        }

        context.Result = new ForbidResult();
    }
}

/// <summary>
/// Универсальный фильтр доступа по ролям.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public sealed class RoleAccessAttribute(params string[] allowedRoles) : ActionFilterAttribute, IOrderedFilter
{
    private readonly HashSet<string> _allowedRoles = allowedRoles
        .Select(r => r.Trim().ToLowerInvariant())
        .Where(r => !string.IsNullOrWhiteSpace(r))
        .ToHashSet();

    public new int Order => -100;

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var role = RoleResolver.GetRole(context);
        if (_allowedRoles.Count == 0 || _allowedRoles.Contains(role))
        {
            base.OnActionExecuting(context);
            return;
        }

        context.Result = new ForbidResult();
    }
}

internal static class RoleResolver
{
    private const string HeaderName = "X-User-Role";
    private const string QueryName = "asRole";

    public static string GetRole(ActionExecutingContext context)
    {
        var request = context.HttpContext.Request;

        var rawRole = request.Headers[HeaderName].FirstOrDefault()
                      ?? request.Query[QueryName].FirstOrDefault()
                      ?? AppRoles.Guest;

        var role = rawRole.Trim().ToLowerInvariant();
        return AppRoles.All.Contains(role) ? role : AppRoles.Guest;
    }
}
