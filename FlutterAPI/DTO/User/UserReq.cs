using System.ComponentModel.DataAnnotations;
using System.Configuration;

namespace FlutterAPI.DTO.User
{
    public class UserReq
    {
        [Required(ErrorMessage = "Vui lòng nhập NumberID")]
        public string? NumberID { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập họ và tên")]
        public string? FullName { get; set; }
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
