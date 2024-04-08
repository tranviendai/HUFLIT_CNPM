using System.ComponentModel.DataAnnotations.Schema;

namespace FlutterAPI.Model
{
    public class Category
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? ImageURL { get; set; }
        public string? Description { get; set; }
        [ForeignKey("UserID")]
        public User? User { get; set; }
        public string? UserID { get; set; }
        public List<Product>? Products { get; set; }
    }
}
