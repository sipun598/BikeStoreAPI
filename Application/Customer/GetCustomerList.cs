using Application.DTOs;
using Application.DTOs.CustomerDtos;
using Application.Helpers;
using AutoMapper;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Persistance;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Customer
{
    public class GetCustomerList
    {
        public class Query : IRequest<List<CustomersDto>>
        {
            public PaginationDTO pagination;

            public Query(HttpContext httpContext, PaginationDTO pagination)
            {
                HttpContext = httpContext;
                this.pagination = pagination;
            }

            public HttpContext HttpContext { get; }
        }

        public class Handler : IRequestHandler<Query, List<CustomersDto>>
        {
            private readonly BikeStoresContext context;
            private readonly IMapper mapper;

            public Handler(BikeStoresContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<List<CustomersDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var queryable = context.Customers.AsQueryable();
                await request.HttpContext.InsertPaginationPaeametersInResponse(queryable, request.pagination);
                var customers = await queryable.Paginate(request.pagination).ToListAsync();

                var customersToReturn = mapper.Map<List<Customers>, List<CustomersDto>>(customers);

                return customersToReturn;
            }
        }
    }
}