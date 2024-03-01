using Test.Domain.DTOs;
namespace Test.Api.Interfaces
{
    public interface IDirectorio
    {
        Task<PersonaModel> GetPersonaById(int id);
        Task<PersonaModel> GetPersonaByIdentificacion(string identificacion);
        Task<List<PersonaModel>> FindPersonas(string match);
        Task<PersonaModel> StorePersona(PersonaModel model);
        Task<bool> DeletePersonaByIdentificacion(string identificacion);
        Task<bool> DeletePersonaById(int id);
        Task<List<PersonaModel>> GetAllAsync(bool faker = false);
    }
}
