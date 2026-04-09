using MyFullstackApp.Domains.Models.Base;
using MyFullstackApp.Domains.Models.Moderation;

namespace MyFullstackApp.BusinessLogic.Interface;

public interface IModerationReport
{
    List<ModerationReportDto> GetAllModerationReportsAction();
    ModerationReportDto? GetModerationReportByIdAction(int id);
    ResponceMsg ResponceModerationReportCreateAction(ModerationReportDto report);
    ResponceMsg ResponceModerationReportUpdateAction(ModerationReportDto report);
    ResponceMsg ResponceModerationReportDeleteAction(int id);
}
