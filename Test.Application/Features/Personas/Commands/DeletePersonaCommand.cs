using Test.Domain.Entities;
using AspNetCoreHero.Results;
using MediatR;
using Microsoft.Extensions.Logging;
using Test.Application.Interfaces.Repositories;
namespace Test.Application.Features.Personas.Commands
{
    public class DeletePersonaCommand : IRequest<Result<bool>>
    {
        public int Id { get; set; }
        public string? Identificacion { get; set; }

        public class DeletePersonaCommandHandler : IRequestHandler<DeletePersonaCommand, Result<bool>>
        {
            private readonly IRepositoryAsync<Persona> _repository;
            private IUnitOfWork _unitOfWork { get; set; }
            private readonly ILogger<DeletePersonaCommand> _logger;

            public DeletePersonaCommandHandler(
                IRepositoryAsync<Persona> repository,
                IUnitOfWork unitOfWork,
                ILogger<DeletePersonaCommand> logger)
            {
                _repository = repository;
                _unitOfWork = unitOfWork;
                _logger = logger;
            }

            public async Task<Result<bool>> Handle(DeletePersonaCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    string msg = string.Empty;

                    await _unitOfWork.BeginTransactionAsync(cancellationToken);

                    Persona entity = null;

                    if (request.Id > 0)
                    {
                        entity = await _repository.GetByIdAsync(request.Id);
                    }
                    else if (!string.IsNullOrEmpty(request.Identificacion))
                    {
                        entity = await _repository.GetFirstOrDefaultAsync(x => x.Identificacion.Equals(request.Identificacion));
                    }

                    if (entity == null)
                    {
                        msg = "Persona no encontrada.";
                    }

                    if (string.IsNullOrEmpty(msg))
                    {
                        await _repository.DeleteAsync(entity);

                        await _unitOfWork.Commit(cancellationToken);
                    }

                    await _unitOfWork.CommitTransactionAsync(cancellationToken);

                    return await Result<bool>.SuccessAsync(true, msg);
                }
                catch (Exception ex)
                {
                    await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                    _logger.LogError(ex.Message);
                    return await Result<bool>.FailAsync(ex.Message);
                }
            }
        }
    }
}
