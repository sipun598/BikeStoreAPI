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

namespace Application.Brand
{
    public class GetBrandList
    {
        public class Query : IRequest<BrandPaginationViewModel>
        {
            public readonly PaginationDto pagination;

            public Query(PaginationDto pagination)
            {
                this.pagination = pagination;
            }
        }

        public class Handler : IRequestHandler<Query, BrandPaginationViewModel>
        {
            private readonly BikeStoresContext context;
            private readonly IMapper mapper;

            public Handler(BikeStoresContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<BrandPaginationViewModel> Handle(Query request, CancellationToken cancellationToken)
            {
                var queryable = context.Brands.AsQueryable();

                int totalCount = await queryable.CountAsync();

                var BrandList = await queryable.Paginate(request.pagination).ToListAsync();

                if (BrandList?.Count == 0)
                {
                    throw new RestException(HttpStatusCode.NotFound, new { Brands = "Not Found" });
                }
                var rtobj = ReturnPaginationDto.GetPage(BrandList, request.pagination.Page, request.pagination.RecordsPerPage, totalCount);

                var BrandssToReturn = mapper.Map<PaginationViewModel<Brands>, BrandPaginationViewModel>(rtobj);

                return BrandssToReturn;
            }
        }
    }
}