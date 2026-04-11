using AutoMapper;
using MyFullstackApp.BusinessLogic.Core.Moderation;
using MyFullstackApp.BusinessLogic.Interface;
using MyFullstackApp.Domains.Models.Base;
using MyFullstackApp.Domains.Models.Moderation;

namespace MyFullstackApp.BusinessLogic.Functions.Moderation;

public class ModerationReportFlow : ModerationReportAction, IModerationReport
{
    public ModerationReportFlow(IMapper mapper) : base(mapper) { }

    public List<ModerationReportDto> GetAllModerationReportsAction() => ExecuteGetAllModerationReportsAction();

    public ModerationReportDto? GetModerationReportByIdAction(int id) => GetModerationReportDataByIdAction(id);

    public ResponceMsg ResponceModerationReportCreateAction(ModerationReportDto report) =>
        ExecuteModerationReportCreateAction(report);

    public ResponceMsg ResponceModerationReportUpdateAction(ModerationReportDto report) =>
        ExecuteModerationReportUpdateAction(report);

    public ResponceMsg ResponceModerationReportDeleteAction(int id) => ExecuteModerationReportDeleteAction(id);
}
