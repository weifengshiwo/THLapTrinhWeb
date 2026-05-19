using System.ComponentModel.DataAnnotations;

namespace GREEN_STORE.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên danh mục là bắt buộc")]
        [StringLength(50, ErrorMessage = "Tên danh mục không quá 50 ký tự")]
        public string Name { get; set; }
    }
}
