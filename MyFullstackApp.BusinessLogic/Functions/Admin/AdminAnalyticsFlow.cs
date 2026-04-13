using MyFullstackApp.BusinessLogic.Core.Admin;
using MyFullstackApp.BusinessLogic.Interface;
using MyFullstackApp.Domains.Models.Admin;

namespace MyFullstackApp.BusinessLogic.Functions.Admin;

public class AdminAnalyticsFlow : AdminAnalyticsAction, IAdminAnalytics
{
    public AdminStatsDto GetAdminStatsAction() => ExecuteGetAdminStatsAction();
}
