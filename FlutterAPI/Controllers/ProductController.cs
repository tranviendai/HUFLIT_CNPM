using FlutterAPI.Data;
using FlutterAPI.DTO.Category;
using FlutterAPI.DTO.Product;
using FlutterAPI.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FlutterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        public FlutterAPIContext db { get; set; }
        public ProductController(FlutterAPIContext db)
        {
            this.db = db;
        }
        [HttpGet("getList")]
        public async Task<IActionResult> list()
        {
            string numberID = HttpContext.User.FindFirstValue("ID")!;
            try
            {
                var data = await db.Product.Include(e=>e.Category).Where(e => e.Category!.UserID == numberID).Select(e => new ProductRes(e)).ToListAsync();
                return this.OkRes(data);
            }
            catch (Exception ex)
            {
                return this.BadRequestRes(ex.Message);
            }
        }
        [HttpPost("Add")]
        public async Task<IActionResult> add([FromForm] ProductReq request)
        {
            string numberID = HttpContext.User.FindFirstValue("ID")!;
            try
            {
                if (request.Name == null) return this.BadRequestRes("Name không được rỗng");
                Product product = new Product()
                {
                    Name = request.Name,
                    Description  = request.Description,
                    Image = request.Image,
                    Price = request.Price,
                    CategoryID = request.CategoryID
                };
                db.Product.Add(product);
                await db.SaveChangesAsync();
                return this.OkRes("Thêm thành công");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update")]
        public async Task<IActionResult> update([FromForm] int id, [FromForm] ProductReq request)
        {
            string numberID = HttpContext.User.FindFirstValue("ID")!;
            try
            {
                var data = await db.Product.Include(e=>e.Category).FirstOrDefaultAsync(e => e.Id == id && e.Category!.UserID == numberID);
                if (data == null) return this.BadRequestRes("Dữ liệu này không tồn tại");
                if (request.Name == null) return this.BadRequestRes("Name không được rỗng");
                data!.Name = request.Name;
                data.Description = request.Description;
                data.Price = request.Price;
                data.Image = request.Image;
                data.CategoryID = request.CategoryID;
                db.Product.Update(data);
                await db.SaveChangesAsync();
                return this.OkRes("Cập nhật thành công");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("remove")]
        public async Task<IActionResult> remove([FromForm] int productID)
        {
            string numberID = HttpContext.User.FindFirstValue("ID")!;
            try
            {
                var data = await db.Product.Include(e=>e.Category).FirstOrDefaultAsync(e => e.Category!.UserID == numberID && productID == e.Id);
                if (data == null) return this.BadRequestRes("Dữ liệu này không tồn tại");
                db.Product.Remove(data);
                await db.SaveChangesAsync();
                return this.OkRes("Xóa thành công");
            }
            catch (Exception ex)
            {
                return this.BadRequestRes(ex.Message);
            }
        }
    }
}
