namespace MudBlazorWebApp1.Models;

public class GrowthData
{
    public DateTime MeasuredAt { get; set; } // تاریخ اندازه‌گیری
    public decimal WeightKg { get; set; }
    public decimal LengthCm { get; set; }
    public decimal HeadCircumferenceCm { get; set; }

    public decimal? Bmi => LengthCm > 0
        ? WeightKg / (decimal)Math.Pow((double)(LengthCm / 100m), 2)
        : null;
}