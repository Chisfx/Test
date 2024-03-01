using Test.Domain.Entities;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Test.Application.Interfaces.Repositories;
using Test.Application.Interfaces.Shared;
using Test.Domain.DTOs;
namespace Test.Application.Features.Personas.Commands
{
    public class UpdatePersonaCommand : IRequest<Result<PersonaModel>>
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = default!;
        public string ApellidoPaterno { get; set; } = default!;
        public string ApellidoMaterno { get; set; } = default!;
        public string Identificacion { get; set; } = default!;

        public class UpdatePersonaCommandHandler : IRequestHandler<UpdatePersonaCommand, Result<PersonaModel>>
        {
            private readonly IRepositoryAsync<Persona> _repository;
            private readonly IMapper _mapper;
            private IUnitOfWork _unitOfWork { get; set; }
            private readonly ICompareObject _compare;
            private readonly ILogger<UpdatePersonaCommand> _logger;

            public UpdatePersonaCommandHandler(
                IRepositoryAsync<Persona> repository,
                IUnitOfWork unitOfWork,
                IMapper mapper,
                ICompareObject compare,
                ILogger<UpdatePersonaCommand> logger)
            {
                _repository = repository;
                _unitOfWork = unitOfWork;
                _mapper = mapper;
                _compare = compare;
                _logger = logger;
            }

            public async Task<Result<PersonaModel>> Handle(UpdatePersonaCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    PersonaModel result = null;
                    string msg = string.Empty;

                    await _unitOfWork.BeginTransactionAsync(cancellationToken);

                    var entity = await _repository.GetByIdAsync(request.Id);

                    if (entity == null)
                    {
                        msg = "Persona no encontrada.";
                    }
                    else
                    {
                        var exist = await _repository.AnyAsync(p => p.Id != request.Id && p.Identificacion == request.Identificacion);
                        if (exist)
                        {
                            msg = $"Identificación {request.Identificacion} ya existe.";
                        }
                        else
                        {
                            if (_compare.Compare(_mapper.Map<PersonaModel>(entity), _mapper.Map<PersonaModel>(request)))
                            {
                                msg = "Debe actualizar los registros.";
                            }
                        }
                    }

                    if (string.IsNullOrEmpty(msg))
                    {
                        _mapper.Map(request, entity);

                        await _repository.UpdateAsync(entity);

                        await _unitOfWork.Commit(cancellationToken);

                        if (entity.Id == 0)
                        {
                            throw new Exception("Error en BD");
                        }

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
