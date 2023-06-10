using ETicaret.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Domain.Entities
{
    public class Order : Entity
    {
        public string UserId { get; set; }
        public decimal TotalPrice { get; set; }
        public bool OrderComplete { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
