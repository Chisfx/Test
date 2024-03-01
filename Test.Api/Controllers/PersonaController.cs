using Microsoft.AspNetCore.Mvc;
using Test.Api.Abstractions;
using Test.Application.Exceptions;
using Test.Domain.DTOs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Test.Api.Controllers
{
    [Route("api/[controller]")]
    public class PersonaController : BaseController
    {
        // GET: api/<PersonaController>
        [HttpGet]
        public async Task<List<PersonaModel>> GetAsync()
        {
            try
            {
                return await _directorio.GetAllAsync();
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message);
            }
        }

        // GET api/<PersonaController>/5
        [HttpGet("{id}")]
        public async Task<PersonaModel> GetByIdAsync(int id)
        {
            try
            {
                return await _directorio.GetPersonaById(id);
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message);
            }
        }

        // GET api/<PersonaController>/ByIdentificacion/5
        [HttpGet("ByIdentificacion/{identificacion}")]
        public async Task<PersonaModel> GetByIdentificacionAsync(string identificacion)
        {
            try
            {
                return await _directorio.GetPersonaByIdentificacion(identificacion);
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message);
            }
        }

        // POST api/<PersonaController>
        [HttpPost]
        public async Task PostAsync(PersonaModel model)
        {
            try
            {
                await _directorio.StorePersona(model);
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message);
            }
        }

        // DELETE api/<PersonaController>/5
        [HttpDelete("{id}")]
        public async Task DeleteByIdAsync(int id)
        {
            try
            {
                await _directorio.DeletePersonaById(id);
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message);
            }
        }

        // DELETE api/<PersonaController>/ByIdentificacion/5
        [HttpGet("ByIdentificacion/{identificacion}")]
        [HttpDelete("ByIdentificacion/{identificacion}")]
        public async Task DeleteByIdentificacionAsync(string identificacion)
        {
            try
            {
                await _directorio.DeletePersonaByIdentificacion(identificacion);
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message);
            }
        }
    }
}
