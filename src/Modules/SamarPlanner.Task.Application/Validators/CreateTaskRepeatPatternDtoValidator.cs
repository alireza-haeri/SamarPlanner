using FluentValidation;
using SamarPlanner.Shared.Contracts.Contracts;
using SamarPlanner.Shared.Contracts.Enums;
using SamarPlanner.Task.Core.Entities;

namespace SamarPlanner.Task.Application.Validators;

public class CreateTaskRepeatPatternDtoValidator : AbstractValidator<RepeatPatternDto>
{
    public CreateTaskRepeatPatternDtoValidator()
    {
        When(x => x != null, () =>
        {
            RuleFor(x => x!.Type)
                .IsInEnum()
                .WithMessage("{PropertyName} باید یکی از مقادیر Daily, WeeklyOnDays, MonthlyOnDays باشد.")
                .WithName("نوع تکرار");

            RuleFor(x => x!.Interval)
                .NotNull()
                .WithMessage("وقتی نوع تکرار برابر Daily است، فاصله تکرار نمی‌تواند null باشد.")
                .GreaterThan(0)
                .WithMessage("{PropertyName} باید بزرگتر از 0 باشد.")
                .When(x => x!.Type == RepeatPatternType.Daily)
                .WithName("فاصله تکرار");

            RuleFor(x => x!.AnchorDate)
                .NotNull()
                .WithMessage("وقتی نوع تکرار برابر Daily است، فاصله تکرار نمی‌تواند null باشد.");

            RuleFor(x => x!.WeekDays)
                .NotNull()
                .WithMessage("وقتی نوع تکرار برابر WeeklyOnDays است، روزهای هفته نمی‌توانند null باشند.")
                .When(x => x!.Type == RepeatPatternType.WeeklyOnDays)
                .WithName("روزهای هفته");

            RuleFor(x => x!.WeekDays)
                .Null()
                .WithMessage("وقتی نوع تکرار برابر WeeklyOnDays نیست، روزهای هفته باید null باشند.")
                .When(x => x!.Type != RepeatPatternType.WeeklyOnDays)
                .WithName("روزهای هفته");

            RuleFor(x => x!.MonthDays)
                .NotNull()
                .WithMessage("وقتی نوع تکرار برابر MonthlyOnDays است، روزهای ماه نمی‌توانند null باشند.")
                .NotEmpty()
                .WithMessage("{PropertyName} برای الگوی MonthlyOnDays نباید خالی باشد.")
                .Must(list => list?.Any() == true)
                .WithMessage("{PropertyName} باید شامل اعداد بین 1 تا 31 باشد.")
                .Must(list => list!.All(day => day is >= 1 and <= 31))
                .WithMessage("{PropertyName} باید شامل اعداد بین 1 تا 31 باشد.")
                .When(x => x!.Type == RepeatPatternType.MonthlyOnDays)
                .WithName("روزهای ماه");

            RuleFor(x => x!.MonthDays)
                .Null()
                .WithMessage("وقتی نوع تکرار برابر MonthlyOnDays نیست، روزهای ماه باید null باشند.")
                .When(x => x!.Type != RepeatPatternType.MonthlyOnDays)
                .WithName("روزهای ماه");
        });
    }
}