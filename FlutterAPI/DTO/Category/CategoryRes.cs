
namespace FlutterAPI.DTO.Category
{
    public class CategoryRes
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public CategoryRes(FlutterAPI.Model.Category category)
        {
            Id = category.Id;
            Name = category.Name;
            Description = category.Desc;
        }
    }
}
