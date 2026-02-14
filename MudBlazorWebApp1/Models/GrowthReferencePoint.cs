using System.ComponentModel.DataAnnotations;

namespace MudBlazorWebApp1.Models;

public enum ReferenceSource
{
    Mashhad = 1,
    Who = 2
}

public enum GrowthMetric
{
    WeightForAge = 1,       // x=Month, y=WeightKg
    LengthForAge = 2,       // x=Month, y=LengthCm
    HeadForAge = 3,         // x=Month, y=HeadCircumferenceCm
    WeightForLength = 4     // x=LengthCm, y=WeightKg
}

public class GrowthReferencePoint
{
    public long Id { get; set; }

    [Required]
    public ReferenceSource Source { get; set; }

    [Required]
    public GrowthMetric Metric { get; set; }

    [Required]
    public ChildGender Gender { get; set; }

    // محور X:
    // برای داده‌های Age-based: ماه (0..60)
    // برای WeightForLength: قد/طول به cm (مثلاً 45..110)
    [Range(0, 300)]
    public decimal X { get; set; }

    // صدک‌ها (محور Y، واحد بسته به Metric)
    public decimal? P1 { get; set; }
    public decimal? P3 { get; set; }
    public decimal? P5 { get; set; }
    public decimal? P15 { get; set; }
    public decimal? P25 { get; set; }
    public decimal? P50 { get; set; }
    public decimal? P75 { get; set; }
    public decimal? P85 { get; set; }
    public decimal? P95 { get; set; }
    public decimal? P97 { get; set; }
    public decimal? P99 { get; set; }
}