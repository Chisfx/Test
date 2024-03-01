using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Test.Api.Interfaces;

namespace Test.Api.Abstractions
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        private IDirectorio _directorioInstance;
        private IVentas _ventasInstance;

        protected IDirectorio _directorio => _directorioInstance ??= HttpContext.RequestServices.GetService<IDirectorio>();
        protected IVentas _ventas => _ventasInstance ??= HttpContext.RequestServices.GetService<IVentas>();

    }
}
