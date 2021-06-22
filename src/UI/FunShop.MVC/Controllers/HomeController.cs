using FunShop.Core.services;
using FunShop.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FunShop.MVC.Controllers
{
    public class HomeController : Controller
    {

        public ProductService ProductService { get; }

        public HomeController(ProductService productService)
        {
            
            ProductService = productService;
        }

        public IActionResult Index(int index=1,int size=8,string search=null)
        {
            var products = ProductService.GetProducts(index,size,search);
            return View(products);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult GetProductById(int id)
        {
            var pr = ProductService.GetProductById(id);
            return View(pr);
        }
        public IActionResult Product()
        {
            var products = ProductService.GetProducts();
            return Ok(products);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        
    }
}
