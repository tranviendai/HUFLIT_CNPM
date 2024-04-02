
namespace FlutterAPI.DTO.Category
{
    public class ProductReq
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ImageURL { get; set; }
        public double Price { get; set; }
        public int? CategoryID { get; set; }
    }
}
