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
    }
}
