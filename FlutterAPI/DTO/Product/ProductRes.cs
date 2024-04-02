using FlutterAPI.DTO.Category;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlutterAPI.DTO.Product
{
    public class ProductRes
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ImageURL { get; set; }
        public double Price { get; set; }
        public int? CategoryID { get; set; }
        public string? CategoryName { get; set; }

        public ProductRes(FlutterAPI.Model.Product product)
        {
            Id = product.Id;
            Name = product.Name;
            Description = product.Description;
            ImageURL = product.ImageURL;
            Price = product.Price;
            CategoryID = product.CategoryID;
            CategoryName = product.Category?.Name;
        }
    }
    public class ProductCategoryRes
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ImageURL { get; set; }
        public double Price { get; set; }

        public ProductCategoryRes(FlutterAPI.Model.Product product)
        {
            Id = product.Id;
            Name = product.Name;
            Description = product.Description;
            ImageURL = product.ImageURL;
            Price = product.Price;
        }
    }
}
