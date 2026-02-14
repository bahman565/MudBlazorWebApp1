using Microsoft.EntityFrameworkCore;
using MudBlazorWebApp1.Data;
using MudBlazorWebApp1.Models;

namespace MudBlazorWebApp1.Services;

public class GrowthReferenceQueryService
{
    private readonly ApplicationDbContext _db;
    public GrowthReferenceQueryService(ApplicationDbContext db) => _db = db;

    public Task<List<GrowthReferencePoint>> GetAsync(
        ReferenceSource source,
        GrowthMetric metric,
        ChildGender gender,
        CancellationToken ct = default)
    {
        return _db.GrowthReferencePoints
            .AsNoTracking()
            .Where(x => x.Source == source && x.Metric == metric && x.Gender == gender)
            .OrderBy(x => x.X)
            .ToListAsync(ct);
    }
}