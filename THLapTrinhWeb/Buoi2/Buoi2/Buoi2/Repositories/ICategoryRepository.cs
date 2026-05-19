using GREEN_STORE.Models;
using System.Collections.Generic;
using GREEN_STORE.Models;

namespace GREEN_STORE.Repositories
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetAllCategories();
    }
}