using MediatR;
using Persistance;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Customer
{
    public class DeleteCustomer 
    {
        public class Command : IRequest
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly BikeStoresContext bikeStoresContext;

            public Handler(BikeStoresContext bikeStoresContext)
            {
                this.bikeStoresContext = bikeStoresContext;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var customer = await bikeStoresContext.Customers.FindAsync(request.Id);

                if (customer == null)
                {
                    throw new Exception("no customer found to delete");
                }

                bikeStoresContext.Remove(customer);
                var sucess = await bikeStoresContext.SaveChangesAsync() > 0;

                if (sucess)
                {
                    return Unit.Value;
                }

                throw new Exception("unable to save changes");
            }
        }
    }
}
