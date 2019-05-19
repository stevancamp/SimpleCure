using System;

namespace BusinessLayer.Models.AdminModels
{
    public class ApplicationLog_Model
    {
        public int ID { get; set; }
        public string IP_Address { get; set; }
        public DateTime ErrorTime { get; set; }
        public string ErrorMessage { get; set; }
    }
}