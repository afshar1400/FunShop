using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FunShop.Core;
using System.Reflection;
using FunShop.Domain.IRepository;
using FunShop.Database.Repository;
using FunShop.Core.Infrastructrue;
using FunShop.MVC.Infrastracture;
using AutoMapper;
namespace FunShop.MVC
{
    public static class DependencyImport
    {
        public static IServiceCollection AddMoreServie(this IServiceCollection @this)
        {
            var Inject = typeof(Inject);
            var definedType = Inject.Assembly.DefinedTypes;

            var services = definedType.Where(x => x.GetTypeInfo().GetCustomAttribute<Inject>() != null);


            foreach (var service in services)
            {
                @this.AddTransient(service);
            }

            @this.AddTransient<IProductRepository, ProductRepository>();
            @this.AddTransient<ICategoryRepository, CategoryRepository>();
            @this.AddTransient<IOrderRepository, OrderRepository>();
            @this.AddTransient<IOrderItemRepository, OrderItemRepository>();
            @this.AddTransient<ISessionManager, SessionManager>();
            @this.AddTransient<IZarinPalManager, ZarinPalManager>();

            //for automapper in core
            @this.AddAutoMapper(typeof(ModelMapping));
            return @this;
        }
    }
}
