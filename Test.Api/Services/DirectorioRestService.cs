using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using Test.Api.Interfaces;
using Test.Application.Exceptions;
using Test.Application.Features.Personas.Commands;
using Test.Application.Features.Personas.Queries;
using Test.Domain.DTOs;
namespace Test.Api.Services
{
    public class DirectorioRestService : IDirectorio
    {
        private readonly IMediator _mediator;
        private readonly ILogger<DirectorioRestService> _logger;
        private readonly IMapper _mapper;
        public DirectorioRestService(IMediator mediator, 
            ILogger<DirectorioRestService> logger,
            IMapper mapper)
        {
            _mediator = mediator;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<bool> DeletePersonaById(int id)
        {
            try
            {
                var response = await _mediator.Send(new DeletePersonaCommand() { Id = id });

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

        public async Task<bool> DeletePersonaByIdentificacion(string identificacion)
        {
            try
            {
                var response = await _mediator.Send(new DeletePersonaCommand() { Identificacion = identificacion });

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

        public async Task<List<PersonaModel>> FindPersonas(string match)
        {
            List<PersonaModel> list = new List<PersonaModel>();
            try
            {
                var response = await _mediator.Send(new GetAllPersonasQuery() { Match = match });

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

        public async Task<List<PersonaModel>> GetAllAsync(bool faker)
        {
            List<PersonaModel> list = new List<PersonaModel>();
            try
            {
                var response = await _mediator.Send(new GetAllPersonasQuery() { Faker = faker });

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

        public async Task<PersonaModel> GetPersonaById(int id)
        {
            PersonaModel entity = new PersonaModel();
            try
            {
                var response = await _mediator.Send(new GetPersonaByIdOrIdentificacionQuery() { Id = id });

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

        public async Task<PersonaModel> GetPersonaByIdentificacion(string identificacion)
        {
            PersonaModel entity = new PersonaModel();
            try
            {
                var response = await _mediator.Send(new GetPersonaByIdOrIdentificacionQuery() { Identificacion = identificacion });

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

        public async Task<PersonaModel> StorePersona(PersonaModel model)
        {
            PersonaModel entity = new PersonaModel();
            try
            {
                if (model != null)
                {
                    Result<PersonaModel> response;

                    if (model.Id > 0)
                    {
                        response = await _mediator.Send(_mapper.Map<UpdatePersonaCommand>(model));
                    }
                    else
                    {
                        response = await _mediator.Send(_mapper.Map<CreatePersonaCommand>(model));
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
