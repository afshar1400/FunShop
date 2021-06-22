using FunShop.Domain.Entity;
using FunShop.Domain.Specifiction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunShop.Domain.IRepository
{
    public interface IOrderRepository
    {
        int AddOrder(Order order);
        IEnumerable<Order> GetAllOrders();
        IEnumerable<Order> GetAllOrders(Paging page);
        Order GetOrderById(int id);
        IEnumerable<orderItemProduct> LoadOrderItems(int id);
        Order SendOrder(int id);
        void UpdateOrder(Order order);
    }
}
