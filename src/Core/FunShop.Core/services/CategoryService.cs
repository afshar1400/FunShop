using AutoMapper;
using FunShop.Domain.Entity;
using FunShop.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunShop.Core.services
{
    [Inject]
    public class CategoryService
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IMapper mapper;

        public CategoryService(ICategoryRepository categoryRepository,IMapper mapper)
        {
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
        }

        public void AddCategory(CategoryVM cat)
        {
            var category = mapper.Map<Category>(cat);
            categoryRepository.AddCategory(category);
        }
        public void DeleteCategory(int id)
        {
            categoryRepository.DeleteCategory(id);
        }
        public IEnumerable<Category> GetAllCategory()
        {
          return  categoryRepository.GetAllCategory();
        }
        public Category GetCategoryById(int id)
        {
            return categoryRepository.GetCategoryById(id);
        }
        public void UpdateCategory(CategoryVM cat)
        {
            var category = mapper.Map<Category>(cat);
            categoryRepository.UpdateCategory(category);
        }

    }

    public class CategoryVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

    }
}
