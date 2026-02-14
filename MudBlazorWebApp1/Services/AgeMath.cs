namespace MudBlazorWebApp1.Services;

public static class AgeMath
{
    public static decimal AgeInMonths(DateOnly birthDate, DateTime measuredAtLocal)
    {
        var measuredDate = DateOnly.FromDateTime(measuredAtLocal);
        var days = measuredDate.DayNumber - birthDate.DayNumber;
        if (days < 0) days = 0;

        // تبدیل تقریبی به ماه (برای نمودار کافی است)
        return (decimal)days / 30.4375m;
    }
}