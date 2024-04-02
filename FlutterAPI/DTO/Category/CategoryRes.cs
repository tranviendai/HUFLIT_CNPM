
using FlutterAPI.DTO.Product;

namespace FlutterAPI.DTO.Category
{
    public class CategoryRes
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? ImageURL { get; set; }
        public string? Description { get; set; }
        public CategoryRes(FlutterAPI.Model.Category category)
        {
            Id = category.Id;
            Name = category.Name;
            ImageURL = category.ImageURL;
            Description = category.Description;
        }
    }
    public class CategoryProductRes
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? ImageURL { get; set; }
        public string? Description { get; set; }
        public List<ProductCategoryRes>? Products { get; set; } = new List<ProductCategoryRes>();
        public CategoryProductRes(FlutterAPI.Model.Category category)
        {
            Id = category.Id;
            Name = category.Name;
            ImageURL = category.ImageURL;
            Description = category.Description;
            Products = category.Products!.Select(e=> new ProductCategoryRes(e)).ToList();
        }
    }
}
