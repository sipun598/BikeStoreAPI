using Application.Brand;
using Domain.Models;
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

        [HttpGet("{Id}")]
        public async Task<ActionResult<List<BrandProductsDto>>> Detils(int Id)
        {
            return await mediator.Send(new GetBrandDetailsById.Query { BrandId = Id });
        }
    }
}