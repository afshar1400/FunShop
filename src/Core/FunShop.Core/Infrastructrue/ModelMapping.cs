using AutoMapper;
using FunShop.Core.services;
using FunShop.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunShop.Core.Infrastructrue
{
    public class ModelMapping:Profile
    {
        public ModelMapping()
        {
            CreateMap<CategoryVM, Category>();
            CreateMap<Category, CategoryVM>();

            CreateMap<Order,OrderVM>();
            CreateMap<OrderVM,Order>();
            
            
            CreateMap<Product,ProductVM>();
            CreateMap<ProductVM,Product>();
        }
    }
}
