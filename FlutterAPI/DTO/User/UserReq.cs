using System.ComponentModel.DataAnnotations;
using System.Configuration;

namespace FlutterAPI.DTO.User
{
    public class UserReq
    {
       
        [Required(ErrorMessage ="Vui lòng nhập họ và tên đệm")]
        public string? FirstName { get; set; }
        [Required(ErrorMessage ="Vui lòng nhập tên")]
        public string? LastName { get; set; }
        [Required(ErrorMessage ="Vui lòng nhập số điện thoại")]
        [StringLength(10)]
        [MinLength(10)]

        public int? PhoneNumber { get; set; }
    }
}
