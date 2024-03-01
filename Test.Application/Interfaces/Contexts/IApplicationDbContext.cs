using Test.Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace Test.Application.Interfaces.Contexts
{
    public interface IApplicationDbContext
    {
        //IDbConnection Connection { get; }
        //bool HasChanges { get; }
        //DatabaseFacade DataBase { get; }
        //EntityEntry Entry(object entity);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        DbSet<Persona> Personas { get; set; }
        DbSet<Factura> Facturas { get; set; }
    }
}
