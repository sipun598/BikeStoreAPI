using Application.Customer;
using Application.DTOs;
using Application.DTOs.CustomerDtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IMediator mediator;

        public CustomersController(IMediator Mediator)
        {
            this.mediator = Mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<CustomersDto>>> List([FromQuery] PaginationDTO pagination)
        {
            return await mediator.Send(new GetCustomerList.Query(HttpContext, pagination));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomersDto>> Details(int id)
        {
            return await mediator.Send(new GetCustomerById.Query { CustomerId = id });
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult<Unit>> Delete(int Id)
        {
            return await mediator.Send(new DeleteCustomer.Command { Id = Id });
        }
    }
}