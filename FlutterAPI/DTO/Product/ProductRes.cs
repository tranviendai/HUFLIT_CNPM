using FlutterAPI.DTO.Category;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlutterAPI.DTO.Product
{
    public class ProductRes
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public double Price { get; set; }
        public CategoryRes? Category { get; set; }

        public ProductRes(FlutterAPI.Model.Product product)
        {
            Id = product.Id;
            Name = product.Name;
            Description = product.Description;
            Image = product.Image;
            Price = product.Price;
            Category = new CategoryRes(product.Category!);
        }
    }
}
