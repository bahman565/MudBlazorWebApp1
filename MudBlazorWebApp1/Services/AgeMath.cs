namespace MudBlazorWebApp1.Services;

public static class AgeMath
{
    public static decimal AgeInMonths(DateOnly birthDate, DateTime measuredAtLocal)
    {
        var measuredDate = DateOnly.FromDateTime(measuredAtLocal);
        var days = measuredDate.DayNumber - birthDate.DayNumber;
        if (days < 0) days = 0;

        // ماه‌های کامل + روزهای ناقص
        var months = days / 30;
        var remainingDays = days % 30;
        var monthsWithFraction = months + (decimal)remainingDays / 30m;

        return Math.Round(monthsWithFraction, 2);
    }
}