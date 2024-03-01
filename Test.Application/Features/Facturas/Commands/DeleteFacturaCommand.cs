using Test.Domain.Entities;
using AspNetCoreHero.Results;
using MediatR;
using Microsoft.Extensions.Logging;
using Test.Application.Interfaces.Repositories;
namespace Test.Application.Features.Facturas.Commands
{
    public class DeleteFacturaCommand : IRequest<Result<bool>>
    {
        public int Id { get; set; }

        public class DeleteFacturaCommandHandler : IRequestHandler<DeleteFacturaCommand, Result<bool>>
        {
            private readonly IRepositoryAsync<Factura> _repository;
            private IUnitOfWork _unitOfWork { get; set; }
            private readonly ILogger<DeleteFacturaCommand> _logger;

            public DeleteFacturaCommandHandler(
                IRepositoryAsync<Factura> repository,
                IUnitOfWork unitOfWork,
                ILogger<DeleteFacturaCommand> logger)
            {
                _repository = repository;
                _unitOfWork = unitOfWork;
                _logger = logger;
            }

            public async Task<Result<bool>> Handle(DeleteFacturaCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    string msg = string.Empty;

                    await _unitOfWork.BeginTransactionAsync(cancellationToken);

                    var entity = await _repository.GetByIdAsync(request.Id);

                    if (entity == null)
                    {
                        msg = "User not found.";
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
