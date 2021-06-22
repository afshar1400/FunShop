using FunShop.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunShop.Domain.IRepository
{
    public interface IOrderItemRepository
    {
        void AddOrderItem(OrderItem orderItem);
        Task UpdateProductStock(int orderId);
    }
}
