using System.ComponentModel.DataAnnotations;
using SamarPlanner.Web.Helpers;
using SamarPlanner.Web.Services;

namespace SamarPlanner.Web.Models;

public class ReportEditorModel
{
    [MaxLength(200, ErrorMessage = "عنوان گزارش نمی‌تواند بیش از 200 کاراکتر باشد")]
    public string? Title { get; set; }

    [Required(ErrorMessage = "متن گزارش الزامی است")]
    public string Note { get; set; } = string.Empty;

    public int? Score { get; set; }

    [Required(ErrorMessage = "تاریخ شروع الزامی است")]
    public string PeriodStart { get; set; } = string.Empty;

    [Required(ErrorMessage = "تاریخ پایان الزامی است")]
    public string PeriodEnd { get; set; } = string.Empty;

    public List<ReportHighlightModel> Highlights { get; set; } = [new()];

    public static ReportEditorModel FromDetail(GetReportDetailQueryResponse detail)
    {
        return new ReportEditorModel
        {
            Title = detail.Title,
            Note = detail.Note,
            Score = detail.Score,
            PeriodStart = detail.PeriodStart.ToPersianDate(),
            PeriodEnd = detail.PeriodEnd.ToPersianDate(),
            Highlights = detail.Highlights.Select(h => new ReportHighlightModel
            {
                Text = h.Text,
                Type = h.Type
            }).DefaultIfEmpty(new ReportHighlightModel()).ToList()
        };
    }

    public ReportEditorModel Clone()
    {
        return new ReportEditorModel
        {
            Title = Title,
            Note = Note,
            Score = Score,
            PeriodStart = PeriodStart,
            PeriodEnd = PeriodEnd,
            Highlights = Highlights.Select(h => new ReportHighlightModel
            {
                Text = h.Text,
                Type = h.Type
            }).DefaultIfEmpty(new ReportHighlightModel()).ToList()
        };
    }
}

public class ReportHighlightModel
{
    public string Text { get; set; } = string.Empty;

    public ReportHighlightDtoType Type { get; set; } = ReportHighlightDtoType.Positive;
}



