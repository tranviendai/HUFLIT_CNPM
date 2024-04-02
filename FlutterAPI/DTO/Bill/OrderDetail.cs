using FlutterAPI.Model;
using System.ComponentModel.DataAnnotations;

namespace FlutterAPI.DTO.Bill
{
    public class OrderDetail
    {
        [Required(ErrorMessage = "productID không được rỗng")]
        public int? productID { get; set; }
        [Required(ErrorMessage = "Số lượng phải lớn hơn 0")]
        public int count { get; set; }
    }
    public class OrderRes
    {
        public int? productID { get; set; }
        public string? ProductName { get; set; }
        public string? ImageURL { get; set; }
        public double? Price { get; set; }
        public int? Count { get; set; }
        public double? Total { get; set; }
        public OrderRes(Order order)
        {
            productID = order.ProductID;
            ProductName = order.Product!.Name;
            ImageURL = order.Product!.ImageURL;
            Price = order.Product!.Price;
            Count = order.Count;
            Total = order.Count * order.Product.Price;
        }
    }
}
