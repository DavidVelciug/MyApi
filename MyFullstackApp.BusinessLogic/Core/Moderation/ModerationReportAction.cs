using AutoMapper;
using MyFullstackApp.DataAccess.Context;
using MyFullstackApp.Domains.Entities.Moderation;
using MyFullstackApp.Domains.Enums;
using MyFullstackApp.Domains.Models.Base;
using MyFullstackApp.Domains.Models.Moderation;

namespace MyFullstackApp.BusinessLogic.Core.Moderation;

public class ModerationReportAction
{
    protected readonly IMapper Mapper;

    protected ModerationReportAction(IMapper mapper)
    {
        Mapper = mapper;
    }

    protected List<ModerationReportDto> ExecuteGetAllModerationReportsAction()
    {
        using var db = new AppDbContext();
        var reports = db.ModerationReports.OrderByDescending(r => r.CreatedAtUtc).ToList();
        var emails = reports
            .Select(r => r.ReporterEmail.Trim().ToLowerInvariant())
            .Where(e => e.Length > 0)
            .Distinct()
            .ToList();

        var nameByEmail = db.UserAccounts
            .AsEnumerable()
            .Where(u => emails.Contains(u.Email.Trim().ToLowerInvariant()))
            .ToDictionary(u => u.Email.Trim().ToLowerInvariant(), u => u.DisplayName);

        return reports.Select(r =>
        {
            var dto = Mapper.Map<ModerationReportDto>(r);
            var key = r.ReporterEmail.Trim().ToLowerInvariant();
            if (nameByEmail.TryGetValue(key, out var dn))
            {
                dto.ReporterDisplayName = dn;
            }

            return dto;
        }).ToList();
    }

    protected ModerationReportDto? GetModerationReportDataByIdAction(int id)
    {
        using var db = new AppDbContext();
        var r = db.ModerationReports.FirstOrDefault(x => x.Id == id);
        return r == null ? null : Mapper.Map<ModerationReportDto>(r);
    }

    protected ResponceMsg ExecuteModerationReportCreateAction(ModerationReportDto report)
    {
        using var db = new AppDbContext();
        if (!db.TimeCapsules.Any(c => c.Id == report.CapsuleId))
        {
            return new ResponceMsg { IsSuccess = false, Message = "Capsule not found." };
        }

        var entity = Mapper.Map<ModerationReportData>(report);
        entity.Id = 0;
        entity.CreatedAtUtc = DateTime.UtcNow;
        entity.Status = ReportStatus.Open;
        entity.Capsule = null!;

        db.ModerationReports.Add(entity);
        db.SaveChanges();

        return new ResponceMsg { IsSuccess = true, Message = "Report was successfully submitted." };
    }

    protected ResponceMsg ExecuteModerationReportUpdateAction(ModerationReportDto report)
    {
        using var db = new AppDbContext();
        var data = db.ModerationReports.FirstOrDefault(x => x.Id == report.Id);
        if (data == null)
        {
            return new ResponceMsg { IsSuccess = false, Message = "Report not found." };
        }

        data.Status = report.Status;
        data.Reason = report.Reason;
        data.ReporterEmail = report.ReporterEmail;

        db.SaveChanges();

        return new ResponceMsg { IsSuccess = true, Message = "Report updated successfully." };
    }

    protected ResponceMsg ExecuteModerationReportDeleteAction(int id)
    {
        using var db = new AppDbContext();
        var data = db.ModerationReports.FirstOrDefault(x => x.Id == id);
        if (data == null)
        {
            return new ResponceMsg { IsSuccess = false, Message = "Report not found." };
        }

        db.ModerationReports.Remove(data);
        db.SaveChanges();

        return new ResponceMsg { IsSuccess = true, Message = "Report deleted successfully." };
    }
}
