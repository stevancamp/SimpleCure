using System.Collections.Generic;

namespace SimpleCure.Models.AdminModels
{
    public class UserRoleManagement_ViewModel : ResponseBase
    {
        public List<string> UserRoles { get; set; }
        public List<string> allRoles { get; set; }
        public string Role { get; set; }
        public int ID { get; set; }
        public string ASPNetUserID { get; set; }

    }
}