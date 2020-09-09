using Application.DTOs;
using Application.Errors;
using AutoMapper;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Product
{
    public class GetProductsByBrandId
    {
        public class Query : IRequest<List<BrandProductsDto>>
        {
            public int BrandId { get; set; }
        }

        public class Handler : IRequestHandler<Query, List<BrandProductsDto>>
        {
            private readonly BikeStoresContext context;
            private readonly IMapper mapper;

            public Handler(BikeStoresContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<List<BrandProductsDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var products = await context
                    .Products
                    .Where(x => x.BrandId == request.BrandId)
                    .Include(x => x.Brand)
                    .ToListAsync();

                if (products == null)
                {
                    throw new RestException(HttpStatusCode.NotFound, new { Product = "Not Found" });
                }

                var productsWithBrand = mapper.Map<List<Products>, List<BrandProductsDto>>(products);

                return productsWithBrand;
            }
        }
    }
}