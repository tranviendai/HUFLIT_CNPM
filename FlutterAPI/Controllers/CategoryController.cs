using FlutterAPI.Data;
using FlutterAPI.DTO.Category;
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
    public class CategoryController : ControllerBase
    {
        public FlutterAPIContext db { get; set; }

        public CategoryController(FlutterAPIContext db) {
            this.db = db;
        }
        [HttpGet("getList")]
        public async Task<IActionResult> list()
        {
            string numberID = HttpContext.User.FindFirstValue("ID")!;
            try
            {
                var data = await db.Category.Where(e => e.UserID == numberID).Select(e=> new CategoryRes(e)).ToListAsync();
                return this.OkRes(data);
            }
            catch (Exception ex)
            {
                return this.BadRequestRes(ex.Message);
            }
        }
        [HttpPost("Add")]
        public async Task<IActionResult> add([FromForm] CategoryReq request)
        {
            string numberID = HttpContext.User.FindFirstValue("ID")!;
            try
            {
                if (request.Name == null) return this.BadRequestRes("Name không được rỗng");
                Category category = new Category()
                {
                    Name = request.Name,
                    Desc = request.Description,
                    UserID = numberID,
                };
                db.Category.Add(category);
                await db.SaveChangesAsync();
                return this.OkRes("Thêm thành công");
            }
            catch (Exception ex)
            {
                return this.BadRequestRes(ex.Message);
            }
        }

        [HttpPut("update")]
        public async Task<IActionResult> update([FromForm] int id,[FromForm] CategoryReq request)
        {
            string numberID = HttpContext.User.FindFirstValue("ID")!;
            try
            {
                var data = await db.Category.FirstOrDefaultAsync(e => e.Id == id && e.UserID == numberID);
                if (data == null) return this.BadRequestRes("Dữ liệu này không tồn tại");
                if (request.Name == null) return this.BadRequestRes("Name không được rỗng");
                data!.Name = request.Name;
                data.Desc = request.Description;
                data.UserID = numberID;
                db.Category.Update(data);
                await db.SaveChangesAsync();
                return this.OkRes("Cập nhật thành công");
            }
            catch (Exception ex)
            {
                return this.BadRequestRes(ex.Message);
            }
        }
        [HttpDelete("remove")]
        public async Task<IActionResult> remove([FromForm] int categoryID)
        {
            string numberID = HttpContext.User.FindFirstValue("ID")!;
            try
            {
                var data = await db.Category.Include(e=>e.Products).FirstOrDefaultAsync(e => e.UserID == numberID && categoryID == e.Id);
                if (data == null) return this.BadRequestRes("Dữ liệu này không tồn tại");
                if(data?.Products?.Count > 0) db.RemoveRange(data.Products!);
                db.Remove(data!);
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
