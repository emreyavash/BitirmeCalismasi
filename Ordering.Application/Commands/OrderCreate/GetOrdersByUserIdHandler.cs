using AutoMapper;
using ETicaret.Domain.Repositories;
using MediatR;
using Ordering.Application.Queries;
using Ordering.Application.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Commands.OrderCreate
{
    public class GetOrdersByUserIdHandler : IRequestHandler<GetOrdersByUserIdQuery,IEnumerable<OrderResponse>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public GetOrdersByUserIdHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderResponse>> Handle(GetOrdersByUserIdQuery request, CancellationToken cancellationToken)
        {
            var orderList = await _orderRepository.GetOrdersByUserId(request.UserId);
            var response = _mapper.Map<IEnumerable<OrderResponse>>(orderList);
            return response;
        }
    }
}
