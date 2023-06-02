using ETicaret.Domain.Entities;
using ETicaret.Domain.Repositories;
using ETicaret.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Ordering.Infrastructure.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(OrderContext _dbContext) : base(_dbContext)
        {
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserId(string userId)
        {
            var orderList = await _dbContext.Orders.Where(x=>x.UserId == userId).ToListAsync();
            return orderList; ;
        }
    }
}
