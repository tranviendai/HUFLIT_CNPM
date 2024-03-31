
namespace FlutterAPI.DTO.Auth
{
    public class CurrentUserRes
    {
        public string? IDNumber { get; set; }
        public string? FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? DateCreated { get; set; }
        public bool? Status { get; set; }


        public CurrentUserRes(FlutterAPI.Model.User parent)
        {
            this.FullName = parent.FullName;
            this.PhoneNumber = parent.PhoneNumber;
            this.IDNumber = parent.Id;
            this.DateCreated = parent.DateCreated;
            this.Status = parent.LockoutEnabled;
        }
    }
}
