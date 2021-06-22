using FunShop.Core.services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FunShop.MVC.Controllers
{
    public class CartController : Controller
    {
        private readonly CartService cartService;

        public CartController(CartService cartService)
        {
            this.cartService = cartService;
        }
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult AddToCart(int id)
        {
            cartService.AddToCart(id);
            return Redirect("/");
        }

        public IActionResult ShowCart()
        {
            var cartItems = cartService.GetCartItem();
            return View(cartItems);
        }

        public IActionResult DeletFromCart(int id)
        {
            cartService.RemoveFromCart(id);
            return RedirectToAction("ShowCart");
        } 
        public IActionResult AddFromCart(int id)
        {
            cartService.AddToCart(id);
            return RedirectToAction("ShowCart");
        }
    }
}
