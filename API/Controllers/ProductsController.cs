using Application.DTOs;
using Application.Product;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator mediator;

        public ProductsController(IMediator Mediator)
        {
            mediator = Mediator;
        }

        [HttpGet]
        public async Task<ActionResult<ProductPaginationViewModel>> ProductList([FromQuery] PaginationDto pagination)
        {
            return await mediator.Send(new GetProductList.Query(HttpContext, pagination));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductsDto>> ProductDetails(int id)
        {
            return await mediator.Send(new GetProductListbyId.Query { ProductId = id });
        }
    }
}