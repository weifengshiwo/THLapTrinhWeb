using System.ComponentModel.DataAnnotations;

namespace GREEN_STORE.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên sản phẩm là bắt buộc")]
        [StringLength(100, ErrorMessage = "Tên sản phẩm không quá 100 ký tự")]
        public string Name { get; set; }

        [Range(0.01, 10000.00, ErrorMessage = "Giá phải nằm trong khoảng từ 0.01 đến 10000")]
        public decimal Price { get; set; }

        public string Description { get; set; }

        public int CategoryId { get; set; }

        // Bổ sung các thuộc tính lưu đường dẫn hình ảnh cho phần nâng cao
        public string? ImageUrl { get; set; } // Hình ảnh đại diện
        public List<string>? ImageUrls { get; set; } // Các hình ảnh phụ kèm theo
    }
}
