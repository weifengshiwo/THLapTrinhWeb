using GREEN_STORE.Models;
using GREEN_STORE.Repositories;
using System.Collections.Generic;
using GREEN_STORE.Models;

namespace GREEN_STORE.Repositories
{
    public class MockCategoryRepository : ICategoryRepository
    {
        private List<Category> _categoryList;

        public MockCategoryRepository()
        {
            // Tạo một số danh mục mẫu ban đầu
            _categoryList = new List<Category>
            {
                new Category { Id = 1, Name = "Laptop" },
                new Category { Id = 2, Name = "Desktop" },
                new Category { Id = 3, Name = "Điện thoại" }
            };
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return _categoryList;
        }
    }
}