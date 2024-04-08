using System.ComponentModel.DataAnnotations;

namespace FlutterAPI.DTO.Category
{
    public class CategoryReq
    {
        [Required]
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ImageURL { get; set; }
        [Required]
        public string? AccountID { get; set; }
    }
}
