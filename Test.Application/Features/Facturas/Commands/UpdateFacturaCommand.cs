using Test.Domain.Entities;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Test.Application.Interfaces.Repositories;
using Test.Application.Interfaces.Shared;
using Test.Domain.DTOs;

namespace Test.Application.Features.Facturas.Commands
{
    public class UpdateFacturaCommand : IRequest<Result<FacturaModel>>
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Monto { get; set; }
        public int PersonaId { get; set; }

        public class UpdateFacturaCommandHandler : IRequestHandler<UpdateFacturaCommand, Result<FacturaModel>>
        {
            private readonly IRepositoryAsync<Factura> _repository;
            private readonly IMapper _mapper;
            private IUnitOfWork _unitOfWork { get; set; }
            private readonly ICompareObject _compare;
            private readonly ILogger<UpdateFacturaCommand> _logger;

            public UpdateFacturaCommandHandler(
                IRepositoryAsync<Factura> repository,
                IUnitOfWork unitOfWork,
                IMapper mapper,
                ICompareObject compare,
                ILogger<UpdateFacturaCommand> logger)
            {
                _repository = repository;
                _unitOfWork = unitOfWork;
                _mapper = mapper;
                _compare = compare;
                _logger = logger;
            }

            public async Task<Result<FacturaModel>> Handle(UpdateFacturaCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    FacturaModel result = null;
                    string msg = string.Empty;

                    await _unitOfWork.BeginTransactionAsync(cancellationToken);

                    var entity = await _repository.GetByIdAsync(request.Id);

                    if (entity == null)
                    {
                        msg = "User not found.";
                    }
                    else
                    {
                        if (_compare.Compare(_mapper.Map<FacturaModel>(entity), _mapper.Map<FacturaModel>(request)))
                        {
                            msg = "Must update records.";
                        }                        
                    }

                    if (string.IsNullOrEmpty(msg))
                    {
                        _mapper.Map(request, entity);

                        await _repository.UpdateAsync(entity);

                        await _unitOfWork.Commit(cancellationToken);

                        if (entity.Id == 0)
                        {
                            throw new Exception("Database Error");
                        }

                        result = _mapper.Map<FacturaModel>(entity);
                    }

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
