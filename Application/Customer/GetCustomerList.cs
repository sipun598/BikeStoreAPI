using Application.DTOs;
using Application.Errors;
using Application.Helpers;
using AutoMapper;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistance;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Customer
{
    public class GetCustomerList
    {
        public class Query : IRequest<CustomerPaginationViewModel>
        {
            public readonly PaginationDto pagination;

            public Query(PaginationDto pagination)
            {
                this.pagination = pagination;
            }
        }

        public class Handler : IRequestHandler<Query, CustomerPaginationViewModel>
        {
            private readonly BikeStoresContext context;
            private readonly IMapper mapper;

            public Handler(BikeStoresContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<CustomerPaginationViewModel> Handle(Query request, CancellationToken cancellationToken)
            {
                var queryable = context.Customers.AsQueryable();

                int totalCount = await queryable.CountAsync();

                var CustomersList = await queryable.Paginate(request.pagination).ToListAsync();

                if (CustomersList?.Count == 0)
                {
                    throw new RestException(HttpStatusCode.NotFound, new { customer = "Not Found" });
                }

                var rtobj = ReturnPaginationDto.GetPage(CustomersList, request.pagination.Page, request.pagination.RecordsPerPage, totalCount);

                var customersToReturn = mapper.Map<PaginationViewModel<Customers>, CustomerPaginationViewModel>(rtobj);

                return customersToReturn;
            }
        }
    }
}