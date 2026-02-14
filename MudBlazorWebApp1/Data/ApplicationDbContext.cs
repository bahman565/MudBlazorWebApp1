using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MudBlazorWebApp1.Models; // <-- ??? ?? ????? ??? ????? ?????? ????? ???

namespace MudBlazorWebApp1.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Child> Children => Set<Child>();
    public DbSet<Measurement> Measurements => Set<Measurement>();
    public DbSet<GrowthReferencePoint> GrowthReferencePoints => Set<GrowthReferencePoint>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<GrowthReferencePoint>()
            .HasIndex(x => new { x.Source, x.Metric, x.Gender, x.X })
            .IsUnique();

        builder.Entity<Measurement>()
            .HasOne(m => m.Child)
            .WithMany(c => c.Measurements)
            .HasForeignKey(m => m.ChildId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}