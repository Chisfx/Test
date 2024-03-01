using AspNetCoreHero.Results;
using AutoMapper;
using Bogus;
using MediatR;
using Test.Api.Interfaces;
using Test.Application.Features.Facturas.Commands;
using Test.Application.Features.Facturas.Queries;
using Test.Application.Features.Personas.Commands;
using Test.Application.Features.Personas.Queries;
using Test.Domain.DTOs;

namespace Test.Api.Services
{
    public class VentasRestService : IVentas
    {
        private readonly IMediator _mediator;
        private readonly ILogger<VentasRestService> _logger;
        private readonly IMapper _mapper;
        public VentasRestService(IMediator mediator,
            ILogger<VentasRestService> logger,
            IMapper mapper)
        {
            _mediator = mediator;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<bool> DeleteById(int id)
        {
            try
            {
                var response = await _mediator.Send(new DeleteFacturaCommand() { Id = id });

                if (!response.Succeeded)
                {
                    throw new Exception(response.Message);
                }
                else if (!string.IsNullOrEmpty(response.Message))
                {
                    throw new Exception(response.Message);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
            return true;
        }

        public async Task<List<FacturaModel>> GetAllAsync()
        {
            List<FacturaModel> list = new List<FacturaModel>();
            try
            {
                var response = await _mediator.Send(new GetAllFacturaQuery());

                if (!response.Succeeded)
                {
                    throw new Exception(response.Message);
                }
                else if (!string.IsNullOrEmpty(response.Message))
                {
                    throw new Exception(response.Message);
                }

                list = response.Data;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
            return list;
        }

        public async Task<FacturaModel> GetById(int id)
        {
            FacturaModel entity = new FacturaModel();
            try
            {
                var response = await _mediator.Send(new GetFacturaByIdQuery() { Id = id });

                if (!response.Succeeded)
                {
                    throw new Exception(response.Message);
                }
                else if (!string.IsNullOrEmpty(response.Message))
                {
                    throw new Exception(response.Message);
                }

                entity = response.Data;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
            return entity;
        }

        public async Task<FacturaModel> GetByPersona(int PersonaId)
        {
            FacturaModel entity = new FacturaModel();
            try
            {
                var response = await _mediator.Send(new GetFacturaByPersonaIdQuery() { PersonaId = PersonaId });

                if (!response.Succeeded)
                {
                    throw new Exception(response.Message);
                }
                else if (!string.IsNullOrEmpty(response.Message))
                {
                    throw new Exception(response.Message);
                }

                entity = response.Data;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
            return entity;
        }

        public async Task<FacturaModel> StorePersona(FacturaModel model)
        {
            FacturaModel entity = new FacturaModel();
            try
            {
                if (model != null)
                {
                    Result<FacturaModel> response;

                    if (model.Id > 0)
                    {
                        response = await _mediator.Send(_mapper.Map<UpdateFacturaCommand>(model));
                    }
                    else
                    {
                        response = await _mediator.Send(_mapper.Map<CreateFacturaCommand>(model));
                    }

                    if (!response.Succeeded)
                    {
                        throw new Exception(response.Message);
                    }
                    else if (!string.IsNullOrEmpty(response.Message))
                    {
                        throw new Exception(response.Message);
                    }

                    entity = response.Data;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
            return entity;
        }
    }
}
