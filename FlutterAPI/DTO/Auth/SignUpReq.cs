using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Configuration;

namespace FlutterAPI.DTO.Auth
{
    public class SignUpReq
    {
        [Required(ErrorMessage = "Vui lòng nhập AccountID")]
        public string? AccountID { get; set; }
        [Required(ErrorMessage = "Vui lòng mật khẩu")]
        public string? Password { get; set; }
        [Required(ErrorMessage = "Vui lòng xác nhận mật khẩu")]
        [Compare(nameof(Password), ErrorMessage = "2 mật khẩu không trùng khớp")]
        public string? ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập họ và tên")]
        public string? FullName { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập NumberID")]
        public string? NumberID { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Vui lòng nhập đúng format")]
        [StringLength(10)]
        [MinLength(10)]
        public string? PhoneNumber { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập Giới tính")]
        public string? Gender { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập nhập đúng format dd/MM/yyyy")]
        public string? BirthDay { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập năm học của bạn ví dụ: 2020-2024")]
        public string? SchoolYear { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập Khóa học của bạn ví dụ: K26")]
        public string? SchoolKey { get; set; }
        public string? ImageURL { get; set; }
    }
}
