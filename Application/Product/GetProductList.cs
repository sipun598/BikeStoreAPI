using Application.DTOs;
using Application.Helpers;
using AutoMapper;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Persistance;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Product
{
    public class GetProductList
    {
        public class Query : IRequest<List<ProductsDto>>
        {
            public PaginationDTO pagination;
            public HttpContext HttpContext { get; }

            public Query(HttpContext httpContext, PaginationDTO pagination)
            {
                HttpContext = httpContext;
                this.pagination = pagination;
            }
        }

        public class Handler : IRequestHandler<Query, List<ProductsDto>>
        {
            private readonly BikeStoresContext context;
            private readonly IMapper mapper;

            public Handler(BikeStoresContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<List<ProductsDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var queryable = context
                    .Products
                    .Include(x => x.Brand)
                    .Include(x => x.Category)
                    .AsQueryable();

                await request.HttpContext.InsertPaginationPaeametersInResponse(queryable, request.pagination);
                var products = await queryable.Paginate(request.pagination).ToListAsync();
                var productsToReturn = mapper.Map<List<Products>, List<ProductsDto>>(products);
                return productsToReturn;
            }
        }
    }
}