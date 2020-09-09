using Application.DTOs;
using Application.Errors;
using AutoMapper;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistance;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Product
{
    public class GetProductListbyId
    {
        public class Query : IRequest<ProductsDto>
        {
            public int ProductId { get; set; }
        }

        public class Handler : IRequestHandler<Query, ProductsDto>
        {
            private readonly BikeStoresContext context;
            private readonly IMapper mapper;

            public Handler(BikeStoresContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<ProductsDto> Handle(Query request, CancellationToken cancellationToken)
            {
                var product = await context
                    .Products
                    .Include(x => x.Brand)
                    .Include(x => x.Category)
                    .FirstOrDefaultAsync(x => x.ProductId == request.ProductId);

                if (product == null)
                {
                    throw new RestException(HttpStatusCode.NotFound, new { customer = "Not Found" });
                }

                var productsToReturn = mapper.Map<Products, ProductsDto>(product);
                return productsToReturn;
            }
        }
    }
}