using Application.DTOs;
using Application.Store;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoresController : ControllerBase
    {
        private readonly IMediator mediator;

        public StoresController(IMediator Mediator)
        {
            mediator = Mediator;
        }

        [HttpGet]
        public async Task<ActionResult<StorePaginationViewModel>> List([FromQuery] PaginationDto pagination)
        {
            return await mediator.Send(new GetStoreList.Query(pagination));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StoresDto>> Details(int id)
        {
            return await mediator.Send(new GetStoreById.Query { StoreId = id });
        }
    }
}