namespace FlutterAPI.DTO.Bill
{
    public class BillRes
    {
        public string Id { get; set; }
        public string? FullName { get; set; }
        public DateTime? DateCreated { get; set; }
        public double Total { get; set; }
        public BillRes(Model.Bill bill)
        {
            Id = bill.BillID!;
            FullName = bill.User.FullName;
            DateCreated = bill.DateTime;
            Total = bill.Total;
        }
    }
}
