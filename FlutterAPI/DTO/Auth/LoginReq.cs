using System.ComponentModel.DataAnnotations;

namespace FlutterAPI.DTO
{
    public class LoginReq
    {
        [Display(Name = "NumberID")]
        [Required(ErrorMessage = "Vui lòng nhập NumberID")]

        public string? Account { get; set; }
        [Display(Name = "Password")]
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [StringLength(100)]
        public string? Password { get; set; }
    }
}
