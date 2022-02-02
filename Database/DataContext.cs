using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Database;

public class DataContext : IdentityDbContext<AppUser>
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<City> Cities => Set<City>();
    public DbSet<CityDetail> CitiesDetails => Set<CityDetail>();
    public DbSet<CityPlace> CitiesPlaces => Set<CityPlace>();
    public DbSet<CategoryCity> CategoriesCities => Set<CategoryCity>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<RefreshToken>()
            .ToTable("RefreshesTokens")
            .HasOne(u => u.User)
            .WithMany(t => t.RefreshTokens)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<City>()
            .ToTable("Cities")
            .HasOne(c => c.Detail)
            .WithOne(d => d.City)
            .HasForeignKey<CityDetail>(d => d.CityId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<CityDetail>()
            .ToTable("CitiesDetails")
            .HasKey(x => x.CityId);
        builder.Entity<CityDetail>()
            .HasOne(c => c.City)
            .WithOne(cd => cd.Detail)
            .HasForeignKey<CityDetail>(cd => cd.CityId);

        builder.Entity<CityPlace>(x =>
        {
            x.ToTable("CitiesPlaces")
                .HasKey(x => x.Id);

            x.HasOne(c => c.City)
                .WithMany(cp => cp.CitiesPlaces)
                .HasForeignKey(c => c.CityId)
                .OnDelete(DeleteBehavior.Cascade);
            x.HasOne(c => c.Category)
                .WithMany(cp => cp.CitiesPlaces)
                .HasForeignKey(c => c.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<CategoryCity>()
            .ToTable("CategoriesCities")
            .HasKey(x => x.Id);
        builder.Entity<CategoryCity>();
    }

    public override int SaveChanges()
    {
        AddTimestamps();
        return base.SaveChanges();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        AddTimestamps();
        return await base.SaveChangesAsync(cancellationToken);
    }

    private void AddTimestamps()
    {
        var entities = ChangeTracker.Entries()
            .Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

        foreach (var entity in entities)
        {
            var now = DateTime.UtcNow; // current datetime

            if (entity.State == EntityState.Added)
            {
                ((BaseEntity)entity.Entity).CreatedAt = now;
            }
            ((BaseEntity)entity.Entity).UpdatedAt = now;
        }
    }
}
