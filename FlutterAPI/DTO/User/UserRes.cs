namespace FlutterAPI.DTO.User
{
    public class UserRes
    {
        public string NumberID { get; set; }
        public string FullName { get; set; }
        public DateTime DateCreated { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool Locked { get; set; }

        public UserRes(FlutterAPI.Model.User user)
        {
            NumberID = user.Id;
            FullName = user.FullName!;
            Email = user.Email!;
            PhoneNumber = user.PhoneNumber!;
            Locked = user.LockoutEnabled;
            DateCreated = DateTime.Now;
        }
    }
}
