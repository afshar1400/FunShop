using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunShop.Domain.Entity
{
    public class Order
    {
        public int Id { get; set; }
        public string Province { get; set; }
        public string city { get; set; }
        public string PostCode { get; set; }
        public string BackInfo { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string userId { get; set; }

        public bool IsPaid { get; set; } = false;
        public bool IsSend { get; set; } = false;
        public int Totol { get; set; }
        public IEnumerable<OrderItem> OrderItems { get; set; }
    }
}
