using GREEN_STORE.Models;
using GREEN_STORE.Repositories;
using System.Collections.Generic;
using System.Linq;
using GREEN_STORE.Models;

namespace GREEN_STORE
    .Repositories
{
    public class MockProductRepository : IProductRepository
    {
        private readonly List<Product> _products;

        public MockProductRepository()
        {
            // Khởi tạo danh sách sản phẩm mẫu ban đầu
            _products = new List<Product>
            {
                new Product { Id = 1, Name = "Laptop Dell XPS", Price = 1500, Description = "A high-end laptop from Dell", CategoryId = 1, ImageUrl = "/images/laptop.jpg" }
            };
        }

        public IEnumerable<Product> GetAll()
        {
            return _products;
        }

        public Product GetById(int id)
        {
            return _products.FirstOrDefault(p => p.Id == id);
        }

        public void Add(Product product)
        {
            // Tự động tăng Id dựa trên Id lớn nhất hiện tại
            product.Id = _products.Any() ? _products.Max(p => p.Id) + 1 : 1;
            _products.Add(product);
        }

        public void Update(Product product)
        {
            var index = _products.FindIndex(p => p.Id == product.Id);
            if (index != -1)
            {
                _products[index] = product;
            }
        }

        public void Delete(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                _products.Remove(product);
            }
        }
    }
}