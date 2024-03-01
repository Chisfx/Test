using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Test.Application.Interfaces.Repositories;
using Test.Domain.DTOs;
using Test.Domain.Entities;
namespace Test.Application.Features.Personas.Commands
{
    public class CreatePersonaCommand : IRequest<Result<PersonaModel>>
    {
        public string Nombre { get; set; } = default!;
        public string ApellidoPaterno { get; set; } = default!;
        public string ApellidoMaterno { get; set; } = default!;
        public string Identificacion { get; set; } = default!;

        public class CreatePersonaCommandHandler : IRequestHandler<CreatePersonaCommand, Result<PersonaModel>>
        {
            private readonly IRepositoryAsync<Persona> _repository;
            private readonly IMapper _mapper;
            private IUnitOfWork _unitOfWork { get; set; }
            private readonly ILogger<CreatePersonaCommand> _logger;

            public CreatePersonaCommandHandler(
                IRepositoryAsync<Persona> repository,
                IUnitOfWork unitOfWork,
                IMapper mapper,
                ILogger<CreatePersonaCommand> logger)
            {
                _repository = repository;
                _unitOfWork = unitOfWork;
                _mapper = mapper;
                _logger = logger;
            }

            public async Task<Result<PersonaModel>> Handle(CreatePersonaCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    PersonaModel result = null;
                    string msg = string.Empty;

                    await _unitOfWork.BeginTransactionAsync(cancellationToken);

                    var exist = await _repository.AnyAsync(p => p.Identificacion == request.Identificacion);
                    if (exist)
                    {
                        msg = $"Identificación {request.Identificacion} ya existe.";
                    }
                    else
                    {
                        var entity = _mapper.Map<Persona>(request);

                        await _repository.AddAsync(entity);

                        await _unitOfWork.Commit(cancellationToken);

                        if (entity.Id == 0) throw new Exception("Error en BD");

                        result = _mapper.Map<PersonaModel>(entity);
                    }

                    await _unitOfWork.CommitTransactionAsync(cancellationToken);

                    return await Result<PersonaModel>.SuccessAsync(result, msg);
                }
                catch (Exception ex)
                {
                    await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                    _logger.LogError(ex.Message);
                    return await Result<PersonaModel>.FailAsync(ex.Message);
                }
            }
        }
    }
}
