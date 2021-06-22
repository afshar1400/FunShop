using FunShop.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunShop.Domain.IRepository
{
    public interface ICategoryRepository
    {
        void AddCategory(Category cat);
        void DeleteCategory(int id);
        IEnumerable<Category> GetAllCategory();
        Category GetCategoryById(int id);
        void UpdateCategory(Category cat);
    }
}
