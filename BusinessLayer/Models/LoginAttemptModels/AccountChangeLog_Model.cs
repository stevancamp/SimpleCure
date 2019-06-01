using System;

namespace BusinessLayer.Models.LoginAttemptModels
{
    public class AccountChangeLog_Model
    {
        public int ID { get; set; }
        public string UserIDChanged { get; set; }
        public string ChangedBy { get; set; }
        public DateTime ChangedDateTime { get; set; }
        public string ChangeFrom { get; set; }
        public string ChangeTo { get; set; }
    }
}
