using System.ComponentModel.DataAnnotations;

namespace MudBlazorWebApp1.Models;

public class Measurement
{
    public long Id { get; set; }

    [Required]
    public int ChildId { get; set; }
    public Child Child { get; set; } = default!;

    [Required]
    public DateTime MeasuredAtUtc { get; set; } // ذخیره استاندارد

    [Range(0.1, 50)]
    public decimal WeightKg { get; set; }

    [Range(10, 120)]
    public decimal LengthCm { get; set; }

    [Range(10, 70)]
    public decimal HeadCircumferenceCm { get; set; }

    public string? Note { get; set; }

    public decimal? Bmi => LengthCm > 0
        ? WeightKg / (decimal)Math.Pow((double)(LengthCm / 100m), 2)
        : null;
}