using MyFullstackApp.Domains.Models.Admin;

namespace MyFullstackApp.BusinessLogic.Interface;

public interface IAdminAnalytics
{
    AdminStatsDto GetAdminStatsAction();
}
