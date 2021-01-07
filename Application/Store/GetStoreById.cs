using Application.DTOs;
using Application.Errors;
using AutoMapper;
using Domain.Models;
using MediatR;
using Persistance;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Store
{
    public class GetStoreById
    {
        public class Query : IRequest<StoresDto>
        {
            public int StoreId { get; set; }
        }

        public class Handler : IRequestHandler<Query, StoresDto>
        {
            private readonly BikeStoresContext context;
            private readonly IMapper mapper;

            public Handler(BikeStoresContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<StoresDto> Handle(Query request, CancellationToken cancellationToken)
            {
                var store = await context.Stores.FindAsync(request.StoreId);

                if (store == null)
                {
                    throw new RestException(HttpStatusCode.NotFound, new { store = "Not Found" });
                }

                var StoreToSend = mapper.Map<Stores, StoresDto>(store);

                return StoreToSend;
            }
        }
    }
}