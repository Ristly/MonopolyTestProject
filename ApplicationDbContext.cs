using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MonopolyTest.Models.DBModels;

namespace MonopolyTest;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Pallet> Pallets { get; set; }

    public virtual DbSet<Box> Boxes { get; set; }




    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //Вставить свою строку подключения в переменную среды(Использовался PostgreSQL)
        optionsBuilder.UseNpgsql(Environment.GetEnvironmentVariable("DBConnectionString"));
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entities = ChangeTracker.Entries()
                .ToList();

        var added = entities.Where(t => t.State == EntityState.Added)
                            .Select(t => t.Entity)
                            .ToList();

        foreach (var entity in added)
        {
            if (entity is Box)
            {
                var box = entity as Box;
                box.Created = box.Created.Date;
                box.Volume = box.Width * box.Height * box.Depth;
                if (box.Pallet is not null)
                {
                    box.Pallet.Weight += box.Weight;
                    box.Pallet.Volume += box.Width * box.Height * box.Depth;

                    if (box.Pallet.ExpirationDate > box.Created.AddDays(100) || box.Pallet.ExpirationDate is null)
                        box.Pallet.ExpirationDate = box.Created.AddDays(100).Date;
                }
            }
            if (entity is Pallet)
            {
                var pallet = entity as Pallet;

                pallet.Volume = pallet.Width * pallet.Height * pallet.Depth;

                if (pallet.Boxes is not null && pallet.Boxes is { Count: > 0 })
                {
                    foreach (var box in pallet.Boxes)
                    {
                        pallet.Volume += box.Width * box.Height * box.Depth;
                        pallet.Weight += box.Weight;
                    }
                }
            }
        }

        var modified = entities.Where(t => t.State == EntityState.Modified)
                            .Select(t => t.Entity)
                            .ToList();

        foreach (var entity in modified)
        {
            if (entity is Box)
            {
                var box = entity as Box;
                box.Volume = box.Width * box.Height * box.Depth;
                if (box.Pallet is not null)
                {
                    box.Pallet.Weight = box.Pallet.Boxes.Sum(x => x.Weight) + 30;

                    box.Pallet.Volume = box.Pallet.Boxes.Sum(x => (x.Weight * x.Depth * x.Height))
                        + box.Pallet.Width * box.Pallet.Height * box.Pallet.Depth;

                    box.Pallet.ExpirationDate = box.Pallet.Boxes.Min(x => x.Created).AddDays(100).Date;
                }
            }
            if (entity is Pallet)
            {
                var pallet = entity as Pallet;

                pallet.Volume = pallet.Width * pallet.Height * pallet.Depth;

                if (pallet.Boxes is not null && pallet.Boxes is { Count: > 0 })
                {

                    pallet.Volume += pallet.Boxes.Sum(x => x.Width * x.Height * x.Depth);
                    pallet.Weight = pallet.Boxes.Sum(x => x.Weight);
                    pallet.ExpirationDate = pallet.Boxes.Min(x => x.Created).AddDays(100).Date;
                }
            }
        }

        var removed = entities.Where(t => t.State == EntityState.Deleted)
                            .Select(t => t.Entity)
                            .ToList();


        foreach (var entity in removed)
        {
            if (entity is Box)
            {
                var box = entity as Box;
                if (box.Pallet is not null)
                {
                    box.Pallet.Weight -= box.Weight;
                    box.Pallet.Volume -= box.Width * box.Height * box.Depth;

                    if (box.Pallet.Boxes.Where(x => x.ID != box.ID).Count() < 1)
                        box.Pallet.ExpirationDate = null;
                    else
                        box.Pallet.ExpirationDate = box.Pallet.Boxes.Min(x => x.Created).AddDays(100).Date;
                }
            }

        }


        return await base.SaveChangesAsync(cancellationToken);
    }


}
