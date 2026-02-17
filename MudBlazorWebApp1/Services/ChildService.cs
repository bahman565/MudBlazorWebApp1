using Microsoft.EntityFrameworkCore;
using MudBlazorWebApp1.Data;
using MudBlazorWebApp1.Models;

namespace MudBlazorWebApp1.Services;

public class ChildService
{
    private readonly ApplicationDbContext _db;

    public ChildService(ApplicationDbContext db) => _db = db;

    public Task<List<Child>> GetMyChildrenAsync(string ownerUserId, CancellationToken ct = default) =>
        _db.Children
            .AsNoTracking()
            .Where(c => c.OwnerUserId == ownerUserId)
            .OrderBy(c => c.Name)
            .ToListAsync(ct);

    public Task<Child?> GetChildAsync(int id, string ownerUserId, CancellationToken ct = default) =>
        _db.Children
            .Include(c => c.Measurements.OrderByDescending(m => m.MeasuredAtUtc))
            .SingleOrDefaultAsync(c => c.Id == id && c.OwnerUserId == ownerUserId, ct);

    public async Task<int> CreateChildAsync(Child child, CancellationToken ct = default)
    {
        _db.Children.Add(child);
        await _db.SaveChangesAsync(ct);
        return child.Id;
    }

    public async Task AddMeasurementAsync(Measurement measurement, CancellationToken ct = default)
    {
        _db.Measurements.Add(measurement);
        await _db.SaveChangesAsync(ct);
    }
    public async Task UpdateChildAsync(Child child, CancellationToken ct = default)
    {
        _db.Children.Update(child);
        await _db.SaveChangesAsync(ct);
    }
    public async Task DeleteChildAsync(int childId, string ownerUserId, CancellationToken ct = default)
    {
        var child = await _db.Children
            .SingleOrDefaultAsync(c => c.Id == childId && c.OwnerUserId == ownerUserId, ct);
        if (child == null)
            return;

        _db.Children.Remove(child);
        await _db.SaveChangesAsync(ct);
    }

    public async Task DeleteMeasurementAsync(long measurementId, string ownerUserId, CancellationToken ct = default)
    {
        var m = await _db.Measurements
            .Include(x => x.Child)
            .SingleOrDefaultAsync(x => x.Id == measurementId && x.Child.OwnerUserId == ownerUserId, ct);

        if (m is null) return;

        _db.Measurements.Remove(m);
        await _db.SaveChangesAsync(ct);
    }
}