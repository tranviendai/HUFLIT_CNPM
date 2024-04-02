using System.ComponentModel.DataAnnotations.Schema;

namespace FlutterAPI.Model
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ImageURL { get; set; }
        public double Price { get; set; }

        [ForeignKey("CategoryID")]
        public Category? Category { get; set; }
        public int? CategoryID { get; set; }
    }
}
