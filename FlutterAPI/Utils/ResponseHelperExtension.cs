using Microsoft.AspNetCore.Mvc;
using System.CodeDom;
using System.Net;

namespace FlutterAPI
{
    public class ResponseData
    {
        public bool success { get; set; }
        public object data { get; set; }
        public string error { get; set; }
    }

    public static class ResponseHelper
    {
        public static IActionResult BaseResponse(this ControllerBase ctrl, int code = 200, string error = "", object data = null, bool success = false)
        {
            var responseData = new ResponseData { data = data, error = error, success = success };
            return ctrl.StatusCode(code, responseData);
        }

        public static IActionResult OkRes(this ControllerBase ctrl, object data = null) => BaseResponse(ctrl, success: true, data: data);
        public static IActionResult BadRequestRes(this ControllerBase ctrl, string error = "Request không hợp lệ") => BaseResponse(ctrl, 400, error);
        public static IActionResult UnauthorizedRes(this ControllerBase ctrl, string error = "Chưa đăng nhập") => BaseResponse(ctrl, 401, error);
        public static IActionResult ForbiddenRes(this ControllerBase ctrl, string error = "Không đủ quyền") => BaseResponse(ctrl, 403, error);
        public static IActionResult NotFoundRes(this ControllerBase ctrl, string error = "Không tìm thấy tài nguyên") => BaseResponse(ctrl, 404, error);
        public static IActionResult InternalServerErrorRes(this ControllerBase ctrl, string error = "Lỗi server") => BaseResponse(ctrl, 500, error);
    }
}
