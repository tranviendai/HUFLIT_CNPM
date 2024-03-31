using Azure.Core;
using FlutterAPI.Data;
using FlutterAPI.DTO;
using FlutterAPI.DTO.Auth;
using FlutterAPI.DTO.User;
using FlutterAPI.Model;
using FlutterAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace FlutterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly FlutterAPIContext _context;
        private readonly UserService _user;
        public AuthController(FlutterAPIContext context, UserService userService)
        {
            _context = context;
            _user = userService;
        }
        [HttpPost]
        [Route("signup")]
        public async Task<IActionResult> SignUp([FromForm] SignUpReq model)
        {
            try
            {
                var hasher = new PasswordHasher<User>();
                if (model.FirstName!.Length < 2) return this.BadRequestRes("vui lòng điền đầy họ và tên đệm");
                if (model.LastName!.Length < 2) return this.BadRequestRes("vui lòng điền tên");
                if (model.Password!.Length < 6) return this.BadRequestRes("vui lòng nhập mật khẩu lớn hơn 6 kí tự");
                User user = new User()
                {
                    Id = model.NumberID!,
                    Email = model.NumberID + "@st.huflit.edu.vn",
                    UserName = model.NumberID,
                    FullName = model.FirstName + " " + model.LastName,
                    DateCreated = DateTime.Now,
                    PhoneNumber = model.PhoneNumber!.ToString()
                };
                user.PasswordHash = hasher.HashPassword(user, model.Password!);
                _context.User.Add(user);
                _context.UserRoles.Add(new IdentityUserRole<string>()
                {
                    UserId = user.Id,
                    RoleId = "STUDENT-ROLE"
                });
                await _context.SaveChangesAsync();
                return this.OkRes("Đăng ký thành công");
            }
            catch(Exception ex)
            {
                return this.BadRequestRes("Đăng ký thất bại "+ex);
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromForm] LoginReq model)
        {
            if (_context.User.FirstOrDefaultAsync(e=> model.Account == e.Id).Result!.LockoutEnabled == true) return this.BadRequestRes("Tài khoản này đã bị khóa");
            var tokenInfo = await _user.Login(model);
            if (tokenInfo == null) return this.UnauthorizedRes("Sai tài khoản hoặc mật khẩu");

            return this.OkRes(tokenInfo);
        }
        [HttpGet("current")]
        [Authorize]
        public async Task<IActionResult> UserLogin()
        {
            string username = HttpContext.User.FindFirstValue(ClaimTypes.Name)!;
            var user = await _user.getCurrentUser(username);
            if (user == null)
            {
                return this.UnauthorizedRes();
            }
            else
            {
                return this.OkRes(user);
            }
        }
       
        [HttpPut("updateProfile")]
        [Authorize]
        public async Task<IActionResult> updateProfile([FromForm] UserReq request)
        {
            try
            {
                if (request.FirstName!.Length < 2) return this.BadRequestRes("vui lòng điền đầy họ và tên đệm");
                if (request.LastName!.Length < 2) return this.BadRequestRes("vui lòng điền tên");
                var user = await _user.findById(HttpContext.User.FindFirstValue("ID")!);
                user!.PhoneNumber = request.PhoneNumber.ToString();
                user.FullName = request.FirstName! + " " + request.LastName!;
                _context.User.Update(user!);
                await _context.SaveChangesAsync();
                return this.OkRes("Cập nhật thành công");
            }
            catch (Exception ex)
            {
                return this.BadRequestRes(ex.ToString());
            }
        }

        [HttpPut("ChangePassword")]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromForm] ChangePasswordReq model)
        {
            string pw = model.NewPassword;
            string opw = model.OldPassword;
            if (pw == opw) return this.BadRequestRes("Mật khẩu mới trùng mật khẩu cũ");

            string username = HttpContext.User.FindFirstValue(ClaimTypes.Name)!;
            var user = await _user.findUserByUsername(username);
            if (user == null) return this.NotFoundRes();

            var checkPw = await _user.checkPassword(user, opw);
            if (!checkPw) return this.BadRequestRes("Sai mật khẩu");

            var hasher = new PasswordHasher<User>();
            user.PasswordHash = hasher.HashPassword(user, pw);
            var updated = await _user.update(user);
            if (!updated) return this.InternalServerErrorRes();

            return this.OkRes();
        }
        [HttpPut("forgetPass")]
        public async Task<IActionResult> forgetPass([FromForm] string phoneNumber, [FromForm] string numberID, [FromForm] string newPass)
        {
            if (newPass.Length < 6) return this.BadRequestRes("Vui lòng nhập mật khẩu > 5 kí tự");
            var hasher = new PasswordHasher<User>();
            var user = await _context.User.FirstOrDefaultAsync(x => x.Id == numberID && x.PhoneNumber == phoneNumber);
            user!.PasswordHash = hasher.HashPassword(user, newPass);
            _context.User.Update(user!);
            await _context.SaveChangesAsync();
            return this.OkRes("Đổi mật khẩu thành công");
        }
   
    }
}
