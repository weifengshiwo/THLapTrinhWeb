using GREEN_STORE.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using GREEN_STORE.Models;
using GREEN_STORE.Repositories;

namespace GREEN_STORE.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        // Tiêm dịch vụ vào thông qua Constructor
        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        // 1. Hiển thị danh sách sản phẩm (Read)
        public IActionResult Index()
        {
            var products = _productRepository.GetAll();
            return View(products);
        }

        // 2. Hiển thị chi tiết một sản phẩm
        public IActionResult Display(int id)
        {
            var product = _productRepository.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // 3. Thêm mới sản phẩm (Form GET)
        public IActionResult Add()
        {
            var categories = _categoryRepository.GetAllCategories();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            return View();
        }

        // 4. Xử lý Thêm mới sản phẩm có đính kèm Upload Ảnh (POST)
        [HttpPost]
        public async Task<IActionResult> Add(Product product, IFormFile imageUrl, List<IFormFile> imageUrls)
        {
            if (ModelState.IsValid)
            {
                // Xử lý lưu ảnh đại diện đơn lẻ
                if (imageUrl != null)
                {
                    product.ImageUrl = await SaveImage(imageUrl);
                }

                // Xử lý lưu nhiều ảnh phụ đi kèm
                if (imageUrls != null && imageUrls.Count > 0)
                {
                    product.ImageUrls = new List<string>();
                    foreach (var file in imageUrls)
                    {
                        product.ImageUrls.Add(await SaveImage(file));
                    }
                }

                _productRepository.Add(product);
                return RedirectToAction("Index"); // Quay về trang danh sách
            }

            // Nếu dữ liệu không hợp lệ, tải lại danh mục cho form
            var categories = _categoryRepository.GetAllCategories();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            return View(product);
        }

        // 5. Cập nhật thông tin sản phẩm (Form GET)
        public IActionResult Update(int id)
        {
            var product = _productRepository.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            var categories = _categoryRepository.GetAllCategories();
            ViewBag.Categories = new SelectList(categories, "Id", "Name", product.CategoryId);
            return View(product);
        }

        // 6. Xử lý Cập nhật sản phẩm (POST)
        [HttpPost]
        public IActionResult Update(Product product)
        {
            if (ModelState.IsValid)
            {
                _productRepository.Update(product);
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // 7. Xác nhận xóa sản phẩm (GET)
        public IActionResult Delete(int id)
        {
            var product = _productRepository.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // 8. Xử lý thực hiện Xóa (POST)
        [HttpPost, ActionName("DeleteConfirmed")]
        public IActionResult DeleteConfirmed(int id)
        {
            _productRepository.Delete(id);
            return RedirectToAction("Index");
        }

        // Hàm bổ trợ lưu file ảnh vào thư mục wwwroot/images
        private async Task<string> SaveImage(IFormFile image)
        {
            // Tạo đường dẫn tuyệt đối đến thư mục chứa ảnh
            var savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", image.FileName);

            // Đảm bảo thư mục tồn tại
            var directory = Path.GetDirectoryName(savePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            using (var fileStream = new FileStream(savePath, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }
            return "/images/" + image.FileName; // Trả về đường dẫn tương đối để lưu vào Model
        }
    }
}