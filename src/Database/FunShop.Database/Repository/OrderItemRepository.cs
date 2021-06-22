using FunShop.Domain.Entity;
using FunShop.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunShop.Database.Repository
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly ShopDbContext _context;

        public OrderItemRepository(ShopDbContext context)
        {
            _context = context;
        }

        public void AddOrderItem(OrderItem orderItem)
        {
           var res= _context.OrderItems.Add(orderItem);
            _context.SaveChanges();
        }


        public async Task UpdateProductStock(int orderId)
        {
            var order =await _context.Orders.FindAsync(orderId);
            try
            {
                if (order == null || order.IsPaid == false)
                    return;
                var orderItems = _context.OrderItems.Where(o => o.OrderId == orderId);
                foreach (var item in orderItems)
                {
                    var product = _context.Products.FirstOrDefault(p => p.Id == item.ProductId).InStock -= item.Qty;
                    _context.Update(product);
                }
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message.ToString());
            }
           
        }
    }
}
