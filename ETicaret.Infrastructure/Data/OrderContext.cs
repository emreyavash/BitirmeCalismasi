﻿using ETicaret.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Infrastructure.Data
{
    public class OrderContext: DbContext
    {
        public OrderContext(DbContextOptions<OrderContext> options):base(options) { 
        }
        public DbSet<Order> Orders { get; set; }
    }
}
