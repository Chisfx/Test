using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Test.Application.Interfaces.Repositories;
using Test.Domain.DTOs;
using Test.Domain.Entities;
namespace Test.Application.Features.Facturas.Commands
{
    public class CreateFacturaCommand : IRequest<Result<FacturaModel>>
    {
        public DateTime Fecha { get; set; }
        public decimal Monto { get; set; }
        public int PersonaId { get; set; }


        public class CreateFacturaCommandHandler : IRequestHandler<CreateFacturaCommand, Result<FacturaModel>>
        {
            private readonly IRepositoryAsync<Factura> _repository;
            private readonly IMapper _mapper;
            private IUnitOfWork _unitOfWork { get; set; }
            private readonly ILogger<CreateFacturaCommand> _logger;

            public CreateFacturaCommandHandler(
                IRepositoryAsync<Factura> repository,
                IUnitOfWork unitOfWork,
                IMapper mapper,
                ILogger<CreateFacturaCommand> logger)
            {
                _repository = repository;
                _unitOfWork = unitOfWork;
                _mapper = mapper;
                _logger = logger;
            }

            public async Task<Result<FacturaModel>> Handle(CreateFacturaCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    FacturaModel result = null;
                    string msg = string.Empty;

                    await _unitOfWork.BeginTransactionAsync(cancellationToken);

                    var entity = _mapper.Map<Factura>(request);

                    await _repository.AddAsync(entity);

                    await _unitOfWork.Commit(cancellationToken);

                    if (entity.Id == 0) throw new Exception("Database Error");

                    result = _mapper.Map<FacturaModel>(entity);
                    

                    await _unitOfWork.CommitTransactionAsync(cancellationToken);

                    return await Result<FacturaModel>.SuccessAsync(result, msg);
                }
                catch (Exception ex)
                {
                    await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                    _logger.LogError(ex.Message);
                    return await Result<FacturaModel>.FailAsync(ex.Message);
                }
            }
        }
    }
}
