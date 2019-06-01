using BusinessLayer.Models.LoginAttemptModels;
using Library.DataModel;

namespace BusinessLayer.Mappings
{
    public class MapChangeLogAccount
    {
        public AccountChangeLog MapToLibrary(AccountChangeLog_Model model)
        {
            AccountChangeLog ChangeLog = new AccountChangeLog();
            ChangeLog.ChangedBy = model.ChangedBy;
            ChangeLog.ChangedDateTime = model.ChangedDateTime;
            ChangeLog.ChangeFrom = model.ChangeFrom;
            ChangeLog.ChangeTo = model.ChangeTo;
            ChangeLog.ID = model.ID;
            ChangeLog.UserIDChanged = model.UserIDChanged;               
            return ChangeLog;
        }

        public AccountChangeLog_Model MapToUI(AccountChangeLog model)
        {
            AccountChangeLog_Model ChangeLog = new AccountChangeLog_Model();
            ChangeLog.ChangedBy = model.ChangedBy;
            ChangeLog.ChangedDateTime = model.ChangedDateTime;
            ChangeLog.ChangeFrom = model.ChangeFrom;
            ChangeLog.ChangeTo = model.ChangeTo;
            ChangeLog.ID = model.ID;
            ChangeLog.UserIDChanged = model.UserIDChanged;
            return ChangeLog;
        }
    }
}
