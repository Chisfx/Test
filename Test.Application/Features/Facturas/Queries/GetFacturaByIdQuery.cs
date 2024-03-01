using Test.Domain.Entities;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Test.Application.Interfaces.Repositories;
using Test.Domain.DTOs;
namespace Test.Application.Features.Facturas.Queries
{
    public class GetFacturaByIdQuery : IRequest<Result<FacturaModel>>
    {
        public int Id { get; set; }

        public class GetFacturaByIdQueryHandler : IRequestHandler<GetFacturaByIdQuery, Result<FacturaModel>>
        {
            private readonly IRepositoryAsync<Factura> _repository;
            private readonly IMapper _mapper;
            private readonly ILogger<GetFacturaByIdQuery> _logger;

            public GetFacturaByIdQueryHandler(
                IRepositoryAsync<Factura> repository,
                IMapper mapper,
                ILogger<GetFacturaByIdQuery> logger)
            {
                _repository = repository;
                _mapper = mapper;
                _logger = logger;
            }

            public async Task<Result<FacturaModel>> Handle(GetFacturaByIdQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    FacturaModel result = null;
                    string msg = string.Empty;

                    var entity = await _repository.GetByIdAsync(request.Id);

                    if (entity == null)
                    {
                        msg = "User not found.";
                    }
                    else
                    {
                        result = _mapper.Map<FacturaModel>(entity);
                    }

                    return Result<FacturaModel>.Success(result, msg);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    return Result<FacturaModel>.Fail(ex.Message);
                }
            }
        }
    }
}
