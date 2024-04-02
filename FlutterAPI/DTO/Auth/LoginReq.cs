using System.ComponentModel.DataAnnotations;

namespace FlutterAPI.DTO
{
    public class LoginReq
    {
        [Required(ErrorMessage = "Vui lòng nhập AccountID")]
        public string? AccountID { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [StringLength(100)]
        public string? Password { get; set; }
    }
}
