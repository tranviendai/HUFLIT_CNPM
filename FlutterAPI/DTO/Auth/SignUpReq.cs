using System.ComponentModel.DataAnnotations;
using System.Configuration;

namespace FlutterAPI.DTO.Auth
{
    public class SignUpReq
    {
        [Required(ErrorMessage = "Vui lòng nhập NumberID")]
        public string? NumberID { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Vui lòng nhập đúng format")]
        [StringLength(10)]
        [MinLength(10)]
        public int? PhoneNumber { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập họ và tên đệm")]
        public string? FirstName { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên")]
        public string? LastName { get; set; }
        [Required(ErrorMessage = "Vui lòng mật khẩu")]
        public string? Password { get; set; }
    }
}
