
using System.ComponentModel.DataAnnotations;

namespace FlutterAPI.DTO.Category
{
    public class ProductReq
    {
        [Required]
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ImageURL { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public int? CategoryID { get; set; }
    }
}
