using AutoMapper;
using FunShop.Core.ViewModel;
using FunShop.Domain.Entity;
using FunShop.Domain.IRepository;
using FunShop.Domain.Specifiction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace FunShop.Core.services
{
    [Inject]
    public class ProductService
    {
        private readonly IProductRepository productRepo;
        private readonly IMapper mapper;

        public ProductService(IProductRepository productRepo,IMapper mapper)
        {
            this.productRepo = productRepo;
            this.mapper = mapper;
        }

        public IEnumerable<ProductVM> GetProducts()
        {
            return productRepo.GetAllProducts().Select(s => new ProductVM {Id=s.Id,InStock=s.InStock,CategoryId=s.CategoryId,Price=s.Price,Name=s.Name,Description=s.Description,ImageName=s.ImageName });
        }

        public ProductVMPaging GetProducts(int index,int size,string search)
        {
            var products = productRepo.GetAllProducts();
            int NumberOfPages = (int)Math.Ceiling(products.Count()/(double)size);
            var pgVM = new Paging {PageIndex=index,PageSize=size,Search=search,NumberOfPages=NumberOfPages };
            var prVM = productRepo.GetAllProducts(pgVM);
            var productWithPaging = new ProductVMPaging {Paging=pgVM,Products=prVM };
            return productWithPaging;
        }

        public void AddProduct(ProductVM pr)
        {
            var pro = mapper.Map<Product>(pr);
            productRepo.AddProduct(pro);
        }
        public void DeleteProduct(int id)
        {
            productRepo.DeleteProduct(id);
        }
        public ProductVM GetProductById(int id)
        {
            var product= productRepo.GetProductById(id);
            var pvm = mapper.Map<ProductVM>(product);
            return pvm;
        }

        public void UpdateProduct(ProductVM pvm )
        {
            var pro = mapper.Map<Product>(pvm);
            productRepo.UpdataProduct(pro);
        }
    }

    public class ProductVM
    {
        public int Id { get; set; }
        public int InStock { get; set; }
        public int Price { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageName { get; set; }
        public int CategoryId { get; set; }

    }
}
