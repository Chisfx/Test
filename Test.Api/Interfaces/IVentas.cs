using Test.Domain.DTOs;
namespace Test.Api.Interfaces
{
    public interface IVentas
    {
        Task<FacturaModel> GetById(int id);
        Task<FacturaModel> GetByPersona(int PersonaId);
        Task<FacturaModel> StorePersona(FacturaModel model);
        Task<bool> DeleteById(int id);
        Task<List<FacturaModel>> GetAllAsync();
    }
}
