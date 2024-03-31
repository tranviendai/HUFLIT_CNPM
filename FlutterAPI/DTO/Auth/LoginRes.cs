namespace FlutterAPI.DTO
{
    public class LoginRes
    {
        public string? token {get; set; }
        public DateTime? expiration {get; set; }
        public string? role {get; set; }
    }
}
