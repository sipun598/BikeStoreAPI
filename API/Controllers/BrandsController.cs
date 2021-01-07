using Application.Brand;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using Application.DTOs;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly IMediator mediator;

        public BrandsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<BrandPaginationViewModel>> List([FromQuery]PaginationDto pagination)
        {
            return await mediator.Send(new GetBrandList.Query(pagination));
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<List<BrandProductsDto>>> Detils(int Id)
        {
            return await mediator.Send(new GetBrandDetailsById.Query { BrandId = Id });
        }
    }
}