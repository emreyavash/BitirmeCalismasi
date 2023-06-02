using MediatR;
using Ordering.Application.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Queries
{
    internal class GetOrdersByUserIdQuery : IRequest<IEnumerable<OrderResponse>>
    {
        public string UserId { get; set; }

        public GetOrdersByUserIdQuery(string userId)
        {
            UserId = userId;
        }
    }
}
