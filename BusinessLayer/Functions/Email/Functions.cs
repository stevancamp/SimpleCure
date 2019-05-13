using Library;
using Library.Email.Methods;
using System.Net.Mail;

namespace BusinessLayer.Functions.Email
{
    public class Functions
    {
        #region Injection
        private EmailMessage _emailMessage;
        public Functions()
        {
            _emailMessage = new EmailMessage();
        }
        #endregion
        //send email
        public ResponseBase SendMail(string To, string Subject, string Message)
        {
            return _emailMessage.SendMessage(To, Subject, Message);

        }

        //send email with attachment
        public ResponseBase SendMailWithAttachment(string To, string Subject, string Message, Attachment attachment)
        {
            return _emailMessage.SendMessageAttachment(To, Subject, Message, attachment);
        }
    }
}
