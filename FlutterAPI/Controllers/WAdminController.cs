using FlutterAPI.Data;
using FlutterAPI.DTO.Auth;
using FlutterAPI.DTO.Category;
using FlutterAPI.DTO.User;
using FlutterAPI.Model;
using FlutterAPI.Services;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FlutterAPI.Controllers
{
    [Route("api/W[controller]")]
    [ApiController]
/*    [Authorize(Roles = "Admin")]
*/    public class WAdminController : ControllerBase
    {
        private readonly FlutterAPIContext _context;
        private readonly UserService _user;
        public WAdminController(FlutterAPIContext context, UserService userService)
        {
            _context = context;
            _user = userService;
        }
   /*     [HttpPost]
        [Route("signUp")]
        public async Task<IActionResult> signUp([FromForm] SignUpReq model, [FromForm] [Required] string role = "Student")
        {
            var res = await _user.signup(model, role);
            return Ok(res);
        }*/

        [HttpGet("listUser")]
        public async Task<IActionResult> listUser()
        {
            var user = await _user.getList();
            return this.OkRes(user);
        }
        [HttpGet("findByNumberID")]
        public async Task<IActionResult> findByID(string accountID)
        {
            var db = await _context.User.FirstOrDefaultAsync(e => e.Id == accountID);
            return Ok(new UserAdminRes(db!));
        }

        [HttpPut("changePassUser")]
        public async Task<IActionResult> changePassUser(string accountID, string newPass)
        {
            if (newPass.Length < 6) return this.BadRequestRes("Vui lòng nhập mật khẩu > 5 kí tự");
            var user = await _user.findById(accountID);
            user!.PasswordHash = newPass;
            _context.User.Update(user!);
            await _context.SaveChangesAsync();
            return this.OkRes("Đổi mật khẩu thành công");
        }
        [HttpPut("activeUser")]
        public async Task<IActionResult> activeUser(string accountID)
        {
            var user = await _user.findById(accountID);
            user!.Active = true;
            _context.User.Update(user!);
            await _context.SaveChangesAsync();
            return this.OkRes("Active tài khoản thành công");
        }
        [HttpPut("locked")]
        public async Task<IActionResult> locked(string accountID)
        {
            var user = await _user.findById(accountID);
            user!.LockoutEnabled = !user.LockoutEnabled;
            _context.User.Update(user!);
            await _context.SaveChangesAsync();
            return this.OkRes(user!.LockoutEnabled ? "Khóa tài khoản thành công" : "Mở tài khoản thành công");
        }
        [HttpDelete("removeUser")]
        public async Task<IActionResult> removeUser(string accountID)
        {
            var user = await _user.findById(accountID);
            _context.User.Remove(user!);
            await _context.SaveChangesAsync();
            return this.OkRes("Xóa tài khoản thành công");
        }
   
        [HttpPost("addCategory")]
        public async Task<IActionResult> addCategory([FromForm] CategoryReq request)
        {
            try
            {
                if (request.Name == null) return this.BadRequestRes("Name không được rỗng");
                Category category = new Category()
                {
                    Name = request.Name,
                    Description = request.Description,
                    ImageURL = request.ImageURL,
                };
                _context.Category.Add(category);
                await _context.SaveChangesAsync();
                return this.OkRes("Thêm thành công");
            }
            catch (Exception ex)
            {
                return this.BadRequestRes(ex.Message);
            }
        }

        [HttpPut("updateCategory")]
        public async Task<IActionResult> updateCategory([FromForm] int id, [FromForm] CategoryReq request)
        {
            try
            {
                var data = await _context.Category.FirstOrDefaultAsync(e => e.Id == id);
                if (data == null) return this.BadRequestRes("Dữ liệu này không tồn tại");
                if (request.Name == null) return this.BadRequestRes("Name không được rỗng");
                data!.Name = request.Name;
                data.ImageURL = request.ImageURL;
                data.Description = request.Description;
                _context.Category.Update(data);
                await _context.SaveChangesAsync();
                return this.OkRes("Cập nhật thành công");
            }
            catch (Exception ex)
            {
                return this.BadRequestRes(ex.Message);
            }
        }
        [HttpDelete("removeCategory")]
        public async Task<IActionResult> removeCategory([FromForm] int categoryID)
        {
            try
            {
                var data = await _context.Category.Include(e => e.Products).FirstOrDefaultAsync(e => categoryID == e.Id);
                if (data == null) return this.BadRequestRes("Dữ liệu này không tồn tại");
                if (data?.Products?.Count > 0) _context.RemoveRange(data.Products!);
                _context.Remove(data!);
                await _context.SaveChangesAsync();
                return this.OkRes("Xóa thành công");
            }
            catch (Exception ex)
            {
                return this.BadRequestRes(ex.Message);
            }
        }
        [HttpPost("addProduct")]
        public async Task<IActionResult> addProduct([FromForm] ProductReq request)
        {
            try
            {
                if (request.Name == null) return this.BadRequestRes("Name không được rỗng");
                Product product = new Product()
                {
                    Name = request.Name,
                    Description = request.Description,
                    ImageURL = request.ImageURL,
                    Price = request.Price,
                    CategoryID = request.CategoryID
                };
                _context.Product.Add(product);
                await _context.SaveChangesAsync();
                return this.OkRes("Thêm thành công");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("updateProduct")]
        public async Task<IActionResult> updateProduct([FromForm] int id, [FromForm] ProductReq request)
        {
            try
            {
                var data = await _context.Product.Include(e => e.Category).FirstOrDefaultAsync(e => e.Id == id);
                if (data == null) return this.BadRequestRes("Dữ liệu này không tồn tại");
                if (request.Name == null) return this.BadRequestRes("Name không được rỗng");
                data!.Name = request.Name;
                data.Description = request.Description;
                data.Price = request.Price;
                data.ImageURL = request.ImageURL;
                data.CategoryID = request.CategoryID;
                _context.Product.Update(data);
                await _context.SaveChangesAsync();
                return this.OkRes("Cập nhật thành công");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("removeProduct")]
        public async Task<IActionResult> removeProduct([FromForm] int productID)
        {
            try
            {
                var data = await _context.Product.Include(e => e.Category).FirstOrDefaultAsync(e => productID == e.Id);
                if (data == null) return this.BadRequestRes("Dữ liệu này không tồn tại");
                _context.Product.Remove(data);
                await _context.SaveChangesAsync();
                return this.OkRes("Xóa thành công");
            }
            catch (Exception ex)
            {
                return this.BadRequestRes(ex.Message);
            }
        }
    }
}
