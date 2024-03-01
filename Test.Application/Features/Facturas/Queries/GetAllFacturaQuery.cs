using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Test.Application.Interfaces.Repositories;
using Test.Domain.DTOs;
using Test.Domain.Entities;
namespace Test.Application.Features.Facturas.Queries
{
    public class GetAllFacturaQuery : IRequest<Result<List<FacturaModel>>>
    {
        public class GetAllFacturasQueryHandler : IRequestHandler<GetAllFacturaQuery, Result<List<FacturaModel>>>
        {
            private readonly IRepositoryAsync<Factura> _repository;
            private readonly IMapper _mapper;
            private readonly ILogger<GetAllFacturaQuery> _logger;

            public GetAllFacturasQueryHandler(
                IRepositoryAsync<Factura> repository,
                IMapper mapper,
                ILogger<GetAllFacturaQuery> logger)
            {
                _repository = repository;
                _mapper = mapper;
                _logger = logger;
            }

            public async Task<Result<List<FacturaModel>>> Handle(GetAllFacturaQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    List<FacturaModel> entities;
                    //if (request.Faker)
                    //{
                    //    var user = new Faker<FacturaModel>()
                    //        .RuleFor(c => c.Name, (k, a) => k.Name.FullName().ClampLength(min: 3, max: 50))
                    //        .RuleFor(c => c.Email, (k, a) => k.Internet.Email(a.Name).ClampLength(min: 5))
                    //        .RuleFor(c => c.Age, k => k.Random.Int(18, 60));

                    //    entities = user.Generate(request.Top);
                    //}
                    //else
                    //{
                        var response = await _repository.GetAllAsync();
                        entities = _mapper.Map<List<FacturaModel>>(response);
                    //}

                    return await Result<List<FacturaModel>>.SuccessAsync(entities);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    return await Result<List<FacturaModel>>.FailAsync(ex.Message);
                }
            }
        }
    }
}
