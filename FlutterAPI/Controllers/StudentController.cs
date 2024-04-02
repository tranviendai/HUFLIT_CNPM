using FlutterAPI.Data;
using FlutterAPI.DTO.Auth;
using FlutterAPI.DTO;
using FlutterAPI.DTO.Category;
using FlutterAPI.DTO.Product;
using FlutterAPI.DTO.User;
using FlutterAPI.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using FlutterAPI.Services;
using Microsoft.CodeAnalysis;
using FlutterAPI.DTO.Bill;
using Microsoft.Identity.Client;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FlutterAPI.Controllers
{
    [Route("api/")]
    [ApiController]
    public class StudentController : ControllerBase
    {

        public FlutterAPIContext _context { get; set; }
        private readonly UserService _user;

        public StudentController(FlutterAPIContext db, UserService user)
        {
            this._context = db;
            _user = user;
        }
        [HttpPost("Student/signUp")]
        public async Task<IActionResult> signUp([FromForm] SignUpReq model)
        {
            return this.OkRes(await _user.signup(model, "Student"));
        }
        [HttpPost]
        [Route("Auth/login")]
        public async Task<IActionResult> Login([FromForm] LoginReq model)
        {
            if (_context.User.FirstOrDefaultAsync(e => model.AccountID == e.Id).Result!.Active == false) return BadRequest("Tài khoản này chưa được kích hoạt");
            if (_context.User.FirstOrDefaultAsync(e => model.AccountID == e.Id).Result!.LockoutEnabled == true) return BadRequest("Tài khoản này đã bị khóa");
            var tokenInfo = await _user.Login(model);
            if (tokenInfo == null) return this.UnauthorizedRes("Sai tài khoản hoặc mật khẩu");

            return this.OkRes(tokenInfo);
        }
        [HttpGet("Auth/current")]
        [Authorize]
        public async Task<IActionResult> UserLogin()
        {
            string username = HttpContext.User.FindFirstValue("Id")!;
            var user = await _user.getCurrentUser(username);
            if (user == null)
            {
                return this.UnauthorizedRes();
            }
            else
            {
                return Ok(user);
            }
        }

        [HttpPut("Auth/updateProfile")]
        [Authorize]
        public async Task<IActionResult> updateProfile([FromForm] UserReq request)
        {
            string username = HttpContext.User.FindFirstValue(ClaimTypes.Name)!;
            try
            {
                int day = int.Parse(request.BirthDay!.Substring(0, 2));
                int month = int.Parse(request.BirthDay.Substring(3, 2));
                int year = int.Parse(request.BirthDay.Substring(6));
                var phone = await _context.User.FirstOrDefaultAsync(e => e.PhoneNumber == request.PhoneNumber && e.Id == username);
                if (phone != null) return this.BadRequestRes("PhoneNumber đã có người khác sử dụng");
                if (request.FullName!.Length < 6) return this.BadRequestRes("vui lòng điền họ và tên");
                var user = await _user.findById(HttpContext.User.FindFirstValue("ID")!);
                user!.PhoneNumber = request.PhoneNumber;
                user.FullName = request.FullName;
                user.ImageURL = request.ImageURL;
                user.Gender = request.Gender;
                user.SchoolKey = request.SchoolKey;
                user.SchoolYear = request.SchoolYear;
                user.BirthDay = new DateTime(year, month, day);
                _context.User.Update(user!);
                await _context.SaveChangesAsync();
                return this.OkRes("Cập nhật thành công");
            }
            catch (Exception ex)
            {
                return this.BadRequestRes(ex.ToString());
            }
        }
        [Authorize]
        [HttpPut("Auth/ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromForm] ChangePasswordReq model)
        {
            string pw = model.NewPassword;
            string opw = model.OldPassword;
            if (pw == opw) return BadRequest("Mật khẩu mới trùng mật khẩu cũ");

            string username = HttpContext.User.FindFirstValue("ID")!;
            var user = await _user.findUserByUsername(username);
            if (user == null) return this.NotFoundRes();

            var checkPw = await _context.User.FirstOrDefaultAsync(e => e.Id == username && e.PasswordHash == opw);
            if (checkPw == null) return BadRequest("Sai mật khẩu");

            user.PasswordHash = model.NewPassword;
            var updated = await _user.update(user);
            if (!updated) return this.InternalServerErrorRes();

            return this.OkRes("Đổi mật khẩu thành công");
        }
        [Authorize]
        [HttpPut("Auth/forgetPass")]
        public async Task<IActionResult> forgetPass([FromForm] string accountID, [FromForm] string numberID, [FromForm] string newPass)
        {
            if (newPass.Length < 6) return this.BadRequestRes("Vui lòng nhập mật khẩu > 5 kí tự");
            var user = await _context.User.FirstOrDefaultAsync(x => x.NumberID == numberID && x.Id == accountID);
            if (user == null) return this.BadRequestRes("AccountID hoặc NumberID không đúng");
            user!.PasswordHash = newPass;
            _context.User.Update(user!);
            await _context.SaveChangesAsync();
            return this.OkRes("Đổi mật khẩu thành công");
        }
        [Authorize]
        [HttpGet("Category/getList")]
        public async Task<IActionResult> getListAll()
        {
            try
            {
                var data = await _context.Category.Select(e => new CategoryRes(e)).ToListAsync();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return this.BadRequestRes(ex.Message);
            }
        }
        [Authorize]
        [HttpGet("Product/getListAll")]
        public async Task<IActionResult> list()
        {
            try
            {
                var data = await _context.Product.Include(e => e.Category).Select(e => new ProductRes(e)).ToListAsync();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return this.BadRequestRes(ex.Message);
            }
        }
        [Authorize]
        [HttpGet("Product/getListByCatId")]
        public async Task<IActionResult> getListByCatId(int categoryID = 1)
        {
            try
            {
                var data = await _context.Product.Include(e => e.Category).Where(e => e.CategoryID == categoryID).Select(e => new ProductRes(e)).ToListAsync();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return this.BadRequestRes(ex.Message);
            }
        }
        [Authorize]
        [HttpPost("Order/addBill")]
        public async Task<IActionResult> addBill([FromBody] List<OrderDetail> orders)
        {
            try
            {
                if (orders.Count() < 1) return BadRequest("List này không được để rỗng");
                string username = HttpContext.User.FindFirstValue("ID")!;
                var user = await _context.User.FirstOrDefaultAsync(e => e.NumberID == username);
                if (username == null) return BadRequest("Vui lòng đăng nhập để call api này");
                string guidRandom = Guid.NewGuid().ToString();
                Bill bill = new Bill()
                {
                    BillID = guidRandom,
                    DateTime = DateTime.Now,
                    User = user!,
                    NumberID = username,
                };
                List<Order> listOrder = [];
                foreach (var item in orders)
                {
                    if (_context.Product.Where(e => e.Id == item.productID).ToList().Count == 0) return BadRequest($"ProductId {item.productID} không tồn tại");
                    if (item.count == 0) return BadRequest($"Vui lòng nhập Count cho ProductID {item.productID} khác 0");
                    Order order = new Order()
                    {
                        BillID = bill.BillID!,
                        Count = item.count,
                        ProductID = item.productID,
                        Bill = bill,
                        Product = _context.Product.FirstOrDefault(e => e.Id == item.productID)
                    };
                    listOrder.Add(order);
                }
                bill!.Total = listOrder.Sum(e => e.Product!.Price * e.Count);
                _context.Bill.Add(bill!);
                _context.Order.AddRange(listOrder);
                await _context.SaveChangesAsync();
                return Ok("Thêm hóa đơn thành công");
            }
            catch(Exception ex)
            {
                return this.BadRequest("Thêm bill thất bại");
            }
        }

        [Authorize]
        [HttpPost("Bill/getByID")]
        public async Task<IActionResult> getByID(string billID)
        {
            var data = await _context.Order.Include(e=>e.Product).Include(e => e.Bill).Where(e => e.BillID == billID).Select(e=> new OrderRes(e)).ToListAsync();
            return Ok(data);
        }

        [Authorize]
        [HttpGet("Bill/getHistory")]
        public async Task<IActionResult> getBill()
        {
            string username = HttpContext.User.FindFirstValue("ID")!;
            var data = await _context.Bill.Include(e=>e.User).Where(e=>e.User.Id == username).Select(e=>new BillRes(e)).ToListAsync();
            return Ok(data);
        }
        [HttpDelete("Bill/remove")]
        [Authorize]
        public async Task<IActionResult> removeBill(string billID)
        {
            string username = HttpContext.User.FindFirstValue("ID")!;
            try
            {
                var bill = await _context.Bill.Include(e=>e.User).FirstOrDefaultAsync(e => e.BillID == billID && e.User.Id == username);
                if (bill == null) return BadRequest("Bill này không tồn tại");
                var data = await _context.Order.Where(e => e.BillID == billID).ToListAsync();
                _context.Remove(bill!);
                _context.RemoveRange(data);
                await _context.SaveChangesAsync();
                return Ok("Xóa bill thành công");
            }catch(Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
    }
    
}
