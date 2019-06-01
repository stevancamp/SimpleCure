using BusinessLayer.Models.LoginAttemptModels;
using Library.DataModel;

namespace BusinessLayer.Mappings
{
    public class MapLoginAttempts
    {
        public AspNetUsersLoginAttempt MapToLibrary(AspNetUsersLoginAttempt_Model model)
        {
            AspNetUsersLoginAttempt LoginAttempt = new AspNetUsersLoginAttempt();
            LoginAttempt.ASPNetUserID = model.ASPNetUserID;
            LoginAttempt.ID = model.ID;
            LoginAttempt.IP_Address = model.IP_Address;
            LoginAttempt.LoginDatetime = model.LoginDatetime;
            LoginAttempt.Message = model.Message;
            LoginAttempt.Success = model.Success;
            return LoginAttempt;
        }

        public AspNetUsersLoginAttempt_Model MapToUI(AspNetUsersLoginAttempt model)
        {
            AspNetUsersLoginAttempt_Model LoginAttempt = new AspNetUsersLoginAttempt_Model();
            LoginAttempt.ASPNetUserID = model.ASPNetUserID;
            LoginAttempt.ID = model.ID;
            LoginAttempt.IP_Address = model.IP_Address;
            LoginAttempt.LoginDatetime = model.LoginDatetime;
            LoginAttempt.Message = model.Message;
            LoginAttempt.Success = model.Success;
            return LoginAttempt;
        }
    }
}
