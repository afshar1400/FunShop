using AutoMapper;
using FunShop.Core.Infrastructrue;
using FunShop.Core.ViewModel;
using FunShop.Domain.Entity;
using FunShop.Domain.IRepository;
using FunShop.Domain.Specifiction;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunShop.Core.services
{
    [Inject]
    public class OrderService
    {
        private readonly IOrderRepository orderRepo;
        private readonly ISessionManager _session;
        private readonly IOrderItemRepository _orderItem;
        private readonly IZarinPalManager zarinPalManager;
        private readonly IMapper mapper;

        public OrderService(IOrderRepository orderRepo, ISessionManager session,IOrderItemRepository orderItem,IZarinPalManager zarinPalManager,IMapper mapper)
        {
            this.orderRepo = orderRepo;
            _session = session;
            _orderItem = orderItem;
            this.zarinPalManager = zarinPalManager;
            this.mapper = mapper;
        }

        /// <summary>
        /// when user fill order form --> new order will be created in db -->orderItem get populated with oderId
        /// </summary>
        /// <param name="ovm"> ORder VM</param>
        /// <returns></returns>
        public int AddOrder(OrderVM ovm)
        {
            var sessionOrder = _session.GetCartItem();
            if (sessionOrder.Count() < 1)
            {
                return 0;
            }

            var total = sessionOrder.Select(s => s.Price * s.Qty).Sum();
            ovm.Totol = total;
            var order = mapper.Map<Order>(ovm);
            var orderId = orderRepo.AddOrder(order);
            //todo orderItem repository -> add orderItem
            foreach (var item in sessionOrder)
            {
                _orderItem.AddOrderItem(new OrderItem {OrderId=orderId,ProductId=item.ProductId,Qty=item.Qty });
            }
            _session.cleanCart();
            return orderId;
        }

        public async Task<string> Gobank(int orderId)
        {
            var order = orderRepo.GetOrderById(orderId);
            if (order != null)
            {
            var result = await zarinPalManager.SendToBank(order.Totol, order.Id);
                if (!string.IsNullOrEmpty(result))
                    return result;
            }
            return null;
        }

        public async Task<bool> PayIsValid(int orderId, string authority)
        {
            var order = orderRepo.GetOrderById(orderId);
            if (order == null)
                return false;
            if (string.IsNullOrEmpty(authority))
                return false;
            var res =await zarinPalManager.ValidateBank(authority, order.Totol);
            if (res)
            {
                order.IsPaid = true;
                order.BackInfo = authority;
                orderRepo.UpdateOrder(order);
                await _orderItem.UpdateProductStock(orderId);
            }
            return res;
        }
        public IEnumerable<OrderVM> GetAllOrders()
        {
            var orders = orderRepo.GetAllOrders()
                .Select(s =>new OrderVM {Id=s.Id,city=s.city,Name=s.Name,PhoneNumber=s.PhoneNumber,PostCode=s.PostCode,Province=s.Province, Totol=s.Totol,userId=s.userId ,IsSend=s.IsSend});
           // var orders = mapper.Map<List<OrderVM>>(ords);
            return orders;

        }
        public OrderVMPaging GetAllOrders(int index,int size,string search)
        {
           
            var products = orderRepo.GetAllOrders();
            int NumberOfPages = (int)Math.Ceiling(products.Count() / (double)size);
            var pgVM = new Paging { PageIndex = index, PageSize = size, Search = search, NumberOfPages = NumberOfPages };
            var orderVM = orderRepo.GetAllOrders(pgVM);
            var orderWithPaging = new OrderVMPaging { Paging = pgVM, Orders = orderVM };
            return orderWithPaging;
            

        }

        public OrderVM SendOrder(int id)
        {
            var od = orderRepo.SendOrder(id);
            var orderVM = mapper.Map<OrderVM>(od);
            //var orderVM = new OrderVM {Id=id,Province=od.Province,city=od.city,Name=od.Name,PhoneNumber=od.PhoneNumber,PostCode=od.PostCode, Totol=od.Totol ,IsSend=od.IsSend};

            return orderVM;
        }

        public IEnumerable<orderItemProductVM> LoadOrderItems(int id)
        {
            var oritms = orderRepo.LoadOrderItems(id).Select(s =>new orderItemProductVM { Id=s.ProductId,Name=s.ProductName,Price=s.ProductPrice,Qty=s.Qty });
            return oritms;

        }
    }


    public class OrderVM
    {
        public int Id { get; set; }
        [Required]
        public string Province { get; set; }
        [Required]
        public string city { get; set; }
        [Required]
        public string PostCode { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public bool IsSend { get; set; }
        public string userId { get; set; }
        public int Totol { get; set; }
    }
    public class orderItemProductVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int Qty { get; set; }
    }

}
