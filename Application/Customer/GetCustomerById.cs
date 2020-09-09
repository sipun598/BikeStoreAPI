using Application.DTOs.CustomerDtos;
using Application.Errors;
using AutoMapper;
using Domain.Models;
using MediatR;
using Persistance;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Customer
{
    public class GetCustomerById
    {
        public class Query : IRequest<CustomersDto>
        {
            public int CustomerId { get; set; }
        }

        public class Handler : IRequestHandler<Query, CustomersDto>
        {
            private readonly BikeStoresContext context;
            private readonly IMapper mapper;

            public Handler(BikeStoresContext context,IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<CustomersDto> Handle(Query request, CancellationToken cancellationToken)
            {
                var customer = await context.Customers.FindAsync(request.CustomerId);

                if (customer == null)
                {
                    throw new RestException(HttpStatusCode.NotFound,new { customer = "Not Found"});
                }
                var customerTosend = mapper.Map<Customers, CustomersDto>(customer);

                return customerTosend;
            }
        }
    }
}
