using FlutterAPI.Data;
using FlutterAPI.DTO;
using FlutterAPI.DTO.Auth;
using FlutterAPI.DTO.User;
using FlutterAPI.Model;
using Humanizer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FlutterAPI.Services
{
    public class UserService
    {
        private readonly FlutterAPIContext db;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;

        public UserService(FlutterAPIContext context, UserManager<User> userManager, IConfiguration configuration)
        {
            db = context;
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<LoginRes?> Login(LoginReq model)
        {
            // var user = await _userManager.FindByNameAsync(model.Username!);
            var user = await db.User.Where(x => x.Id == model.AccountID && x.PasswordHash == model.Password).FirstOrDefaultAsync();
            if (user == null) return null;
            if (user != null)
            {
                var role = await getUserRole(user);

                return GetLoginResult(user.PhoneNumber!, user.Id, role);
            }
            else
            {
                return null;
            }
        }
        public async Task<string> signup(SignUpReq model, string role)
        {
            try
            {
                //24/04/2001
                int day = int.Parse(model.BirthDay!.Substring(0, 2));
                int month = int.Parse(model.BirthDay.Substring(3,2));
                int year = int.Parse(model.BirthDay.Substring(6));
                var account = await db.User.FirstOrDefaultAsync(e => e.Id == model.AccountID);
                var phone = await db.User.FirstOrDefaultAsync(e => e.PhoneNumber == model.PhoneNumber);
                var numberID = await db.User.FirstOrDefaultAsync(e => e.NumberID == model.NumberID);
                if (account != null) return "AccountID đã tồn tại";
                if (phone != null) return "PhoneNumber đã tồn tại";
                if (numberID != null) return "NumberID đã tồn tại";
                if (model.FullName!.Length < 4) return "vui lòng điền đầy họ và tên đệm";
                if (model.Password!.Length < 6) return "vui lòng nhập mật khẩu lớn hơn 6 kí tự";
                User user = new User()
                {
                    Id = model.AccountID!.Replace(" ",""),
                    NumberID = model.NumberID!.Replace(" ", ""),
                    ImageURL = model.ImageURL,
                    Email = model.AccountID + "@st.huflit.edu.vn",
                    UserName = model.NumberID!.Trim(),
                    FullName = model.FullName,
                    DateCreated = DateTime.Now,
                    PhoneNumber = model.PhoneNumber!.Replace(" ", "").ToString(),
                    PasswordHash = model.Password.Replace(" ", ""),
                    BirthDay = new DateTime(year,month,day),
                    Gender = model.Gender!.Replace(" ", ""),
                    SchoolYear = model.SchoolYear,
                    SchoolKey = model.SchoolKey!.Replace(" ", ""),
                    Active = true
                };
                db.User.Add(user);
                db.UserRoles.Add(new IdentityUserRole<string>()
                {
                    UserId = user.Id,
                    RoleId = role
                });
                await db.SaveChangesAsync();
                return "Đăng ký thành công";
            }
            catch (Exception ex)
            {
                return "Đăng ký thất bại "+ex;
            }
        }

        public LoginRes GetLoginResult(string accountID, string id, string role)
        {
            var authClaims = new List<Claim>
            {
                    new Claim(ClaimTypes.Name, accountID), //accountID
                    new Claim("ID", id), //numberID
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Role, role)
                };

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]!));

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:ValidIssuer"],
                audience: _configuration["Jwt:ValidAudience"],
                expires: DateTime.Now.AddMonths(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            var encryptedToken = new JwtSecurityTokenHandler().WriteToken(token);

            return new LoginRes()
            {
                token = encryptedToken,
                expiration = token.ValidTo,
                role = role
            };
        }

        public async Task<string> getUserRole(User user)
            => (await _userManager.GetRolesAsync(user)).FirstOrDefault()!;

        //username -> numberID
        public async Task<User?> findUserByUsername(string username)
            => await db.User.Where(x => x.Id == username).FirstOrDefaultAsync();

        public async Task<object?> getCurrentUser(string username)
        {
            var user = await findUserByUsername(username);
            if (user == null) return user;

            var role = await getUserRole(user);
            if (role == "Admin")
            {
                return new { user = "Admin" };
            }
            else if (role == "Student")
            {
                var auth = await db.User.Where(x => x.Id == user.Id).FirstOrDefaultAsync();
                if (auth == null) return auth;
                return new CurrentUserRes(auth);
            }
            else
            {
                return null;
            }
        }

        public async Task<List<UserAdminRes>> getList()
        {
            return await db.User.Where(e => e.UserName != "Admin").Select(e => new UserAdminRes(e)).ToListAsync();
        }

        public async Task<User?> findById(string id)
            => await db.User.Where(x => x.Id == id).FirstOrDefaultAsync();


        public async Task<bool> update(User user)
        {
            try
            {
                db.Entry(user).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
