using BusinessLayer.Interface;
using BusinessLayer.Mappings;
using BusinessLayer.Models;
using Library.Email.Methods;
using System.Net.Mail;

namespace BusinessLayer.Functions.Email
{
    public class EmailFunctions : IEmail
    {
        #region Injection
        private EmailMessage _emailMessage;
        private MapResponseBase _mapResponseBase;
        public EmailFunctions()
        {
            _emailMessage = new EmailMessage();
            _mapResponseBase = new MapResponseBase();
        }
        #endregion
        //send email
        public ResponseBase SendMail(string To, string Subject, string Message)
        {
            return _mapResponseBase.MapToUI(_emailMessage.SendMessage(To, Subject, Message));

        }

        //send email with attachment
        public ResponseBase SendMailWithAttachment(string To, string Subject, string Message, Attachment attachment)
        {
            return _mapResponseBase.MapToUI(_emailMessage.SendMessageAttachment(To, Subject, Message, attachment));
        }
    }
}
