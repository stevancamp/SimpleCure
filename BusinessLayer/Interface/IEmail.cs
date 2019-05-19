using BusinessLayer.Models;
using System.Net.Mail;

namespace BusinessLayer.Interface
{
    public interface IEmail
    {
        ResponseBase SendMail(string To, string Subject, string Message);

        ResponseBase SendMailWithAttachment(string To, string Subject, string Message, Attachment attachment);
    }
}
