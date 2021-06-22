using FunShop.Core.services;
using FunShop.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FunShop.MVC.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        private readonly CategoryService categoryService;

        private readonly ProductService productService;

        public AdminController(CategoryService categoryService,ProductService productService)
        {
            this.categoryService = categoryService;
            this.productService = productService;
        }
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// category crud in admin area
        /// </summary>
        /// <returns></returns>
        public IActionResult GetAllCategory() => Ok(categoryService.GetAllCategory());
        public IActionResult GetCategoryById(int id) => Ok(categoryService.GetCategoryById(id));

        [HttpDelete]
        public IActionResult DeleteCategory(int id) {
            categoryService.DeleteCategory(id);
            return Ok();
        }
        [HttpPost]
        public IActionResult UpdateCategory([FromBody] CategoryVM categoryVM) {
            categoryService.UpdateCategory(categoryVM);
            return Ok();
        }
        [HttpPost]
        public IActionResult AddCategory([FromBody] CategoryVM categoryVM) {
            categoryService.AddCategory(categoryVM);
            return Ok();
        }

        /// <summary>
        /// crud for product
        /// </summary>
        /// <returns></returns>
        public IActionResult GetAllProduct() => Ok(productService.GetProducts());
        public IActionResult GetProductById(int id) => Ok(productService.GetProductById(id));

        [HttpPost]
        [Consumes("application/json", "multipart/form-data")]
        public IActionResult AddProduct([FromForm] ProductWithFileVM withFileVM)
        {
            string ImageName = SaveImageToImageFolder(withFileVM.Image);
            var productVm = new ProductVM { ImageName = ImageName, Name = withFileVM.Name, Description = withFileVM.Description, CategoryId = withFileVM.CategoryId, InStock = withFileVM.InStock, Price = withFileVM.Price };
            productService.AddProduct(productVm);
            return Ok();
        }

        
        [HttpPost]
       // [Consumes("application/json", "multipart/form-data")]
        public IActionResult UpdateProduct([FromForm] ProductWithFileVM withFileVM)
        {
            string ImageName = SaveImageToImageFolder(withFileVM.Image);
            var productVm = new ProductVM {Id=withFileVM.Id, Name = withFileVM.Name, Description = withFileVM.Description, CategoryId = withFileVM.CategoryId, InStock = withFileVM.InStock, Price = withFileVM.Price };

            if (ImageName != null)
                productVm.ImageName = ImageName;

            productService.UpdateProduct(productVm);
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteProduct(int id)
        {
            productService.DeleteProduct(id);
            return Ok();
        }


        /// <summary>
        /// for upload image to image folder under wwwroot
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static string SaveImageToImageFolder(IFormFile image)
        {
            string ImageName = null;
            if (image != null)
            {
                ImageName = Guid.NewGuid().ToString() + image.FileName;
                string ImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", ImageName);

                using (var stream = new FileStream(ImagePath, FileMode.Create))
                {
                    image.CopyTo(stream);
                }
            }
            return ImageName;
        }
    }
}
