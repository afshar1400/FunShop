using FunShop.Core.services;
using FunShop.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FunShop.MVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class OrderController : Controller
    {
        private readonly  OrderService orderService;

        public OrderController(OrderService orderService)
        {
            this.orderService= orderService;
        }
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Index(OrderVM orderVM)
        {
            if (ModelState.IsValid)
            {

            string usrId = User?.FindFirstValue(ClaimTypes.NameIdentifier);
            orderVM.userId = usrId;
            int orderId= orderService.AddOrder(orderVM);
                if (orderId > 0)
                    return RedirectToAction("GoBank",new { orderId });
                else
                    ModelState.AddModelError(" ", "add product to cart again");
            }
            return View();
        }

        [AllowAnonymous]
        public async Task<IActionResult> GoBank(int orderId)
        {

            var res =await orderService.Gobank(orderId);
            if (res == null)
            {
                return RedirectToAction("ErrMsg","Order");
            }
            return Redirect(res);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Thankyou(int id,string Authority, string Status)
        {
            var res = await orderService.PayIsValid(id, Authority);
            var resultVM = new ThankyouVM {BankInfo=id.ToString(),Result=res };
            return View("Thankyou",resultVM);
        }
     
        public IActionResult GetAllOrders()
        {
            var orders = orderService.GetAllOrders();
            return Ok(orders);
        }
   
        //public IActionResult GetAllOrders(int index,int size,string search)
        //{
        //    var orders = orderService.GetAllOrders(index,size,search);
        //    return Ok(orders);
        //}

        public IActionResult SendOrder(int id)
        {
            return Ok(orderService.SendOrder(id));
        }
    
        public IActionResult LoadOrderItems(int id)
        {
            var oritems = orderService.LoadOrderItems(id);
            return Ok(oritems);
        }
        [AllowAnonymous]
        public IActionResult ErrMsg(string errMsg)
        {
            if (errMsg == null)
            {
                errMsg = "try again later";
            }
            var errmsg = new ErrMsgVM { ErrMsg = errMsg, ErrCode = 0 };
            return View(errmsg);
        }
    }
}
