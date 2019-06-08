namespace SimpleCure.Models.AdminModels
{
    public class UserMaintenance_ViewModel : ResponseBase
    {
        public int ID { get; set; }
        public string Email { get; set; }
        public string Company { get; set; }
        public string CustomerName { get; set; }
        public string StreetAddress { get; set; }
        public string MainPhoneNumber { get; set; }
        public bool HasLogin { get; set; }
    }
}