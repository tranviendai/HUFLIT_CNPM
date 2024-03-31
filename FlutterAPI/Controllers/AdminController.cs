using FlutterAPI.Data;
using FlutterAPI.Model;
using FlutterAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlutterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly FlutterAPIContext _context;
        private readonly UserService _user;
        public AdminController(FlutterAPIContext context, UserService userService)
        {
            _context = context;
            _user = userService;
        }

        [HttpGet("listUser")]
        public async Task<IActionResult> listUser()
        {
            var user = await _user.getList();
            return this.OkRes(user);
        }
        [HttpPut("changePassUser")]
        public async Task<IActionResult> changePassUser(string numberID, string newPass)
        {
            if (newPass.Length < 6) return this.BadRequestRes("Vui lòng nhập mật khẩu > 5 kí tự");
            var hasher = new PasswordHasher<User>();
            var user = await _user.findById(numberID);
            user!.PasswordHash = hasher.HashPassword(user, newPass);
            _context.User.Update(user!);
            await _context.SaveChangesAsync();
            return this.OkRes("Đổi mật khẩu thành công");
        }
        [HttpGet("locked")]
        public async Task<IActionResult> locked(string numberID)
        {
            var user = await _user.findById(numberID);
            user!.LockoutEnabled = !user.LockoutEnabled;
            _context.User.Update(user!);
            await _context.SaveChangesAsync();
            return this.OkRes(user!.LockoutEnabled ? "Khóa tài khoản thành công" : "Mở tài khoản thành công");
        }
        [HttpDelete("removeUser")]
        public async Task<IActionResult> removeUser(string numberID)
        {
            var user = await _user.findById(numberID);
            var data = await _context.Category.Include(e => e.Products).Where(e=>e.UserID == numberID).ToListAsync();
            foreach(var category in data)
            {
                _context.Category.Remove(category);
                _context.Product.RemoveRange(category.Products!);
            }
            _context.User.Remove(user!);
            await _context.SaveChangesAsync();
            return this.OkRes("Xóa tài khoản thành công");
        }
    }
}
