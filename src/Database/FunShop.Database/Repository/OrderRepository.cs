using FunShop.Domain.Entity;
using FunShop.Domain.IRepository;
using FunShop.Domain.Specifiction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunShop.Database.Repository
{
    public class OrderRepository:IOrderRepository
    {
        private readonly ShopDbContext _context;

        public OrderRepository(ShopDbContext context)
        {
            _context = context;
        }


        public int AddOrder(Order order)
        {
            var orderResult = _context.Orders.Add(order);
            _context.SaveChanges();
            return orderResult.Entity.Id;
        }

        public Order GetOrderById(int id)
        {
            var order = _context.Orders.FirstOrDefault(x => x.Id == id&& x.IsPaid==false);
            return order;
        }

        public void UpdateOrder(Order order)
        {
            _context.Orders.Update(order);
            _context.SaveChanges();
        }

        public IEnumerable<Order> GetAllOrders()
        {
            var orders = _context.Orders.Where(o => o.IsPaid).OrderBy(s => s.Id);
            return orders;
        }
        public IEnumerable<Order> GetAllOrders(Paging page)
        {
            IEnumerable<Order> orders = _context.Orders.Where(o => o.IsPaid).OrderBy(s => s.Id);

            if(!string.IsNullOrEmpty(page.Search))
            {
                orders = orders.Where(s => s.Name.Contains(page.Search));
            }

            orders =orders.Skip((page.PageIndex-1)*page.PageSize).Take(page.PageSize).ToList();
            return orders;
        }

        public Order SendOrder(int id)
        {
            var order = _context.Orders.Find(id);
            order.IsSend = true;
            _context.Orders.Update(order);
            _context.SaveChanges();
            return order;
        }

        
       public IEnumerable<orderItemProduct> LoadOrderItems(int id)
        {
            var orderItems = _context.OrderItems.Where(x => x.OrderId == id).Include(x => x.Product);
            List<orderItemProduct> orderItemProducts = new List<orderItemProduct>();
            foreach (var item in orderItems)
            {
                orderItemProducts.Add(new orderItemProduct {ProductId=item.ProductId,ProductName=item.Product.Name,ProductPrice=item.Product.Price,Qty=item.Qty });
            }
            return orderItemProducts;
        }
    }
}
