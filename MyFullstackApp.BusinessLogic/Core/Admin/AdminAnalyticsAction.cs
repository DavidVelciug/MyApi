using MyFullstackApp.DataAccess.Context;
using MyFullstackApp.Domains.Models.Admin;

namespace MyFullstackApp.BusinessLogic.Core.Admin;

public class AdminAnalyticsAction
{
    protected AdminStatsDto ExecuteGetAdminStatsAction()
    {
        using var db = new AppDbContext();

        var userRegistrationsByDay = db.UserAccounts
            .GroupBy(u => u.CreatedAtUtc.Date)
            .Select(g => new TimeSeriesPointDto
            {
                Date = g.Key,
                Count = g.Count()
            })
            .OrderBy(x => x.Date)
            .ToList();

        var capsulesCreatedByDay = db.TimeCapsules
            .GroupBy(c => c.CreatedAtUtc.Date)
            .Select(g => new TimeSeriesPointDto
            {
                Date = g.Key,
                Count = g.Count()
            })
            .OrderBy(x => x.Date)
            .ToList();

        return new AdminStatsDto
        {
            UserRegistrationsByDay = userRegistrationsByDay,
            CapsulesCreatedByDay = capsulesCreatedByDay
        };
    }
}