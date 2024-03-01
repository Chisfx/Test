using Test.Application.Interfaces.Contexts;
using Test.Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace Test.Infrastructure.DbContexts
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Persona> Personas { get; set; }
        public DbSet<Factura> Facturas { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            foreach (var property in builder.Model.GetEntityTypes()
            .SelectMany(t => t.GetProperties())
            .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                property.SetColumnType("decimal(18,8)");
            }

            base.OnModelCreating(builder);

            builder.HasDefaultSchema("dbo");

            builder.Entity<Persona>(entity =>
            {
                entity.ToTable(name: "Personas");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Nombre).HasMaxLength(50);
                entity.Property(e => e.ApellidoPaterno).HasMaxLength(50);
                entity.Property(e => e.ApellidoMaterno).HasMaxLength(50);
                entity.Property(e => e.Identificacion).HasMaxLength(50);
                entity.HasIndex(e => e.Identificacion).IsUnique();
            });

            builder.Entity<Factura>(entity =>
            {
                entity.ToTable(name: "Facturas");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity
                .HasOne(c => c.Persona)
                .WithMany()
                .HasForeignKey(a => a.PersonaId)
                .HasPrincipalKey(b => b.Id);
            });
        }
    }
}
