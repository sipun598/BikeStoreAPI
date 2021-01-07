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

namespace Application.Store
{
    public class GetStoreList
    {
        public class Query : IRequest<StorePaginationViewModel>
        {
            public readonly PaginationDto pagination;

            public Query(PaginationDto pagination)
            {
                this.pagination = pagination;
            }
        }

        public class Handler : IRequestHandler<Query, StorePaginationViewModel>
        {
            private readonly BikeStoresContext context;
            private readonly IMapper mapper;

            public Handler(BikeStoresContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<StorePaginationViewModel> Handle(Query request, CancellationToken cancellationToken)
            {
                var queryable = context.Stores.AsQueryable();

                int totalCount = await queryable.CountAsync();

                var StoresList = await queryable.Paginate(request.pagination).ToListAsync();

                if (StoresList?.Count == 0)
                {
                    throw new RestException(HttpStatusCode.NotFound, new { stores = "Not Found" });
                }

                var rtobj = ReturnPaginationDto.GetPage(StoresList, request.pagination.Page, request.pagination.RecordsPerPage, totalCount);

                var storesToReturn = mapper.Map<PaginationViewModel<Stores>, StorePaginationViewModel>(rtobj);

                return storesToReturn;
            }
        }
    }
}