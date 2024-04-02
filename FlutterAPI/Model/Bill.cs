using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlutterAPI.Model
{
    public class Bill
    {
        [Key]
        public string? BillID { get; set; }
        public DateTime DateTime { get; set; }
        public double Total { get; set; }
        public List<Order> Orders { get; set; } = new List<Order>();
        [ForeignKey("NumberID")]
        public required User User { get; set; }
        public string? NumberID { get; set; }
    
    }
    public class Order
    {
        public int Id { get; set; }
        public int Count { get; set; }
        [ForeignKey("BillID")]
        public Bill Bill { get; set; }
        public string BillID { get; set; }

        [ForeignKey("ProductID")]
        public Product? Product { get; set; }
        public int? ProductID { get; set; }
    }
}
