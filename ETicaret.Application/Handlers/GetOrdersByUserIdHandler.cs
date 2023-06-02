using MediatR;
using Ordering.Application.Queries;
using Ordering.Application.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Handlers
{
    public class GetOrdersByUserIdHandler : IRequestHandler<GetOrdersByUserIdQuery, IEnumerable<OrderResponse>>
    {
        Task<IEnumerable<OrderResponse>> IRequestHandler<GetOrdersByUserIdQuery, IEnumerable<OrderResponse>>.Handle(GetOrdersByUserIdQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
