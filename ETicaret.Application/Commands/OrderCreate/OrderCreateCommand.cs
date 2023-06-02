using MediatR;
using Ordering.Application.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Commands.OrderCreate
{
    public class OrderCreateCommand : IRequest<OrderResponse>
    {
        public string ProductId { get; set; }
        public string UserId { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal UnitPrice { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
