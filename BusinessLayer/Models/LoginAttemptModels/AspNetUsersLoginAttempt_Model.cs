namespace BusinessLayer.Models.LoginAttemptModels
{
    public class AspNetUsersLoginAttempt_Model
    {
        public int ID { get; set; }
        public string ASPNetUserID { get; set; }
        public string IP_Address { get; set; }
        public System.DateTime LoginDatetime { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
