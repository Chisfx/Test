using Microsoft.AspNetCore.Mvc;
using Test.Api.Abstractions;
using Test.Api.Interfaces;
using Test.Application.Exceptions;
using Test.Domain.DTOs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Test.Api.Controllers
{
    [Route("api/[controller]")]
    public class FacturaController : BaseController
    {
        // GET: api/<FacturaController>
        [HttpGet]
        public async Task<List<FacturaModel>> GetAsync()
        {
            try
            {
                return await _ventas.GetAllAsync();
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message);
            }
        }

        // GET api/<FacturaController>/5
        [HttpGet("{id}")]
        public async Task<FacturaModel> GetAsync(int id)
        {
            try
            {
                return await _ventas.GetById(id);
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message);
            }
        }

        // POST api/<FacturaController>
        [HttpPost]
        public async Task PostAsync(FacturaModel model)
        {
            try
            {
                await _ventas.StorePersona(model);
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message);
            }
        }

        // DELETE api/<FacturaController>/5
        [HttpDelete("{id}")]
        public async Task DeleteAsync(int id)
        {
            try
            {
                await _ventas.DeleteById(id);
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message);
            }
        }

        // GET api/<FacturaController>/ByPersonaId/5
        [HttpGet("ByPersonaId/{id}")]
        public async Task<FacturaModel> GetByPersonaIdAsync(int id)
        {
            try
            {
                return await _ventas.GetById(id);
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message);
            }
        }
    }
}
