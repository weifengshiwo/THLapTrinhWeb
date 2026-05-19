using GREEN_STORE.Models;
using System.Collections.Generic;
using GREEN_STORE.Models;

namespace GREEN_STORE.Repositories
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();
        Product GetById(int id);
        void Add(Product product);
        void Update(Product product);
        void Delete(int id);
    }
}