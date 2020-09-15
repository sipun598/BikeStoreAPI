using Application.DTOs;
using Application.Errors;
using Application.Helpers;
using AutoMapper;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Persistance;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Product
{
    public class GetProductList
    {
        public class Query : IRequest<ProductPaginationViewModel>
        {
            public PaginationDTO pagination;
            public HttpContext HttpContext { get; }

            public Query(HttpContext httpContext, PaginationDTO pagination)
            {
                HttpContext = httpContext;
                this.pagination = pagination;
            }
        }

        public class Handler : IRequestHandler<Query, ProductPaginationViewModel>
        {
            private readonly BikeStoresContext context;
            private readonly IMapper mapper;

            public Handler(BikeStoresContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<ProductPaginationViewModel> Handle(Query request, CancellationToken cancellationToken)
            {
                var queryable = context
                    .Products
                    .Include(x => x.Brand)
                    .Include(x => x.Category)
                    .AsQueryable();

                int count = await queryable.CountAsync();

                var products = await queryable.Paginate(request.pagination).ToListAsync();
                if (products.Count == 0)
                {
                    throw new RestException(HttpStatusCode.NotFound, new { product = "Not Found" });
                }
                //jenkin3
                var paginationObj = ReturnPaginationDto.GetPage(products, request.pagination.Page, request.pagination.RecordsPerPage, count);
                var productsToReturn = mapper.Map<PaginationViewModel<Products>, ProductPaginationViewModel>(paginationObj);
                return productsToReturn;
            }
        }
    }
}