using FlutterAPI.Data;
using FlutterAPI.DTO;
using FlutterAPI.DTO.Auth;
using FlutterAPI.DTO.User;
using FlutterAPI.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
            var user = await db.User.Where(x => x.Id == model.Account).FirstOrDefaultAsync();
            if (user == null) return null;
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password!))
            {
                var role = await getUserRole(user);

                return GetLoginResult(user.PhoneNumber!, user.Id, role);
            }
            else
            {
                return null;
            }
        }

        public LoginRes GetLoginResult(string numberID, string id, string role)
        {
            var authClaims = new List<Claim>
            {
                    new Claim(ClaimTypes.Name, numberID), //phone
                    new Claim("ID", id), //numberID
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Role, role)
                };

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]!));

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:ValidIssuer"],
                audience: _configuration["Jwt:ValidAudience"],
                expires: DateTime.Now.AddHours(24),
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

        //username -> phone
        public async Task<User?> findUserByUsername(string username)
            => await db.User.Where(x => x.PhoneNumber == username).FirstOrDefaultAsync();

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

        public async Task<List<UserRes>> getList()
            => await db.User.Where(e=> e.UserName != "Admin").Select(e=> new UserRes(e)).ToListAsync();

        public async Task<User?> findById(string id)
            => await db.User.Where(x => x.Id == id).FirstOrDefaultAsync();

        public async Task<bool> add(User user)
        {
            try
            {
                db.User.Add(user);
                await db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }

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

        public async Task<bool> delete(User user)
        {
            try
            {
                db.User.Remove(user);
                await db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<bool> checkPassword(User user, string pw)
            => await _userManager.CheckPasswordAsync(user, pw);

    }
}
