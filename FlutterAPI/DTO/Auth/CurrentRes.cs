
namespace FlutterAPI.DTO.Auth
{
    public class CurrentUserRes
    {
        public string? IDNumber { get; set; }
        public string? AccountID { get; set; }
        public string? FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? ImageURL { get; set; }
        public DateTime? BirthDay { get; set; }
        public string? Gender { get; set; }
        public string? SchoolYear { get; set; }
        public string? SchoolKey { get; set; }
        public DateTime? DateCreated { get; set; }
        public bool? Status { get; set; }

        public CurrentUserRes(FlutterAPI.Model.User user)
        {
            FullName = user.FullName;
            AccountID = user.Id;
            BirthDay = user.BirthDay;
            Gender = user.Gender;
            SchoolKey = user.SchoolKey;
            SchoolYear = user.SchoolYear;
            PhoneNumber = user.PhoneNumber;
            ImageURL = user.ImageURL;
            IDNumber = user.Id;
            DateCreated = user.DateCreated;
            Status = user.LockoutEnabled;
        }
    }
}
