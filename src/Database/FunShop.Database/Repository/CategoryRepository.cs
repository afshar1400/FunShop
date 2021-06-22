using FunShop.Domain.Entity;
using FunShop.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunShop.Database.Repository
{
  

    public class CategoryRepository : ICategoryRepository
    {
        private readonly ShopDbContext context;

        public CategoryRepository(ShopDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Category> GetAllCategory()
        {
            var cats = context.Categories.OrderBy(c => c.Id);
            return cats;
        }

        public Category GetCategoryById(int id)
        {

            var cat = context.Categories.FirstOrDefault(c => c.Id == id);
            if (cat == null)
                return null;
            return cat;
        }

        public void DeleteCategory(int id)
        {
            var cat = context.Categories.FirstOrDefault(c => c.Id == id);
            context.Categories.Remove(cat);
            context.SaveChanges();
        }

        public void AddCategory(Category cat)
        {
            context.Categories.Add(cat);
            context.SaveChanges();
        }

        public void UpdateCategory(Category cat)
        {
            context.Categories.Update(cat);
            context.SaveChanges();
        }
    }
}
