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
using System.Threading;
using System.Threading.Tasks;

namespace Application.Brand
{
    public class GetBrandDetailsById
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
                var Brand = await context
                    .Brands
                    .Include(x => x.Products)
                    .Where(x => x.BrandId == request.BrandId)
                    .ToListAsync();

                if (Brand == null)
                {
                    throw new RestException(HttpStatusCode.NotFound, new { Brand = "Not Found" });
                }

                var BrandProducts = mapper.Map<List<Brands>, List<BrandProductsDto>>(Brand);

                return BrandProducts;
            }
        }
    }
}