using Library.ErrorLogging.Methods;
using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;

namespace Library.Email.Methods
{
    public class EmailMessage
    {
        #region Injection
        private ApplicationError _applicaitonError;

        public EmailMessage()
        {
            _applicaitonError = new ApplicationError();
        }
        #endregion

        public ResponseBase SendMessage(string To, string Subject, string Message)
        {
            ResponseBase response = new ResponseBase();

            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient smtpServer = new SmtpClient(ConfigurationManager.AppSettings["EmailHost"]);
                mail.Subject = Subject;
                mail.From = new MailAddress(ConfigurationManager.AppSettings["EmailAddress"]);
                mail.To.Add(To);
                mail.Body = Message;
                smtpServer.Port = Convert.ToInt32(ConfigurationManager.AppSettings["EmailPortNumber"]);
                smtpServer.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["EmailAddress"], ConfigurationManager.AppSettings["EmailPassword"]);
                smtpServer.EnableSsl = true;
                smtpServer.Send(mail);
            }
            catch (Exception ex)
            {                
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                string source = ex.Source;
                string stacktrace = ex.StackTrace;
                string targetsite = ex.TargetSite.ToString();
                string error = ex.InnerException.ToString();
                string ErrorMessage = $"There was an error at {DateTime.Now} {Environment.NewLine} Method: {methodName} {Environment.NewLine} Source: {source} {Environment.NewLine} StackTrace: {stacktrace} {Environment.NewLine} TargetSite: {targetsite} {Environment.NewLine} Error: {error}{Environment.NewLine} To: {To}{Environment.NewLine} Subject: {Subject}{Environment.NewLine} Message: {Message} ";
                _applicaitonError.Log(ErrorMessage, string.Empty);                       
            }

            return response;
        }

        public ResponseBase SendMessageAttachment(string To, string Subject, string Message, Attachment attachment)
        {
            ResponseBase response = new ResponseBase();

            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient smtpServer = new SmtpClient(ConfigurationManager.AppSettings["EmailHost"]);
                mail.Subject = Subject;
                mail.From = new MailAddress(ConfigurationManager.AppSettings["EmailAddress"]);
                mail.To.Add(To);
                mail.Body = Message;
                mail.Attachments.Add(attachment);
                smtpServer.Port = Convert.ToInt32(ConfigurationManager.AppSettings["EmailPortNumber"]);
                smtpServer.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["EmailAddress"], ConfigurationManager.AppSettings["EmailPassword"]);
                smtpServer.EnableSsl = true;
                smtpServer.Send(mail);
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                string source = ex.Source;
                string stacktrace = ex.StackTrace;
                string targetsite = ex.TargetSite.ToString();
                string error = ex.InnerException.ToString();
                string ErrorMessage = $"There was an error at {DateTime.Now} {Environment.NewLine} Method: {methodName} {Environment.NewLine} Source: {source} {Environment.NewLine} StackTrace: {stacktrace} {Environment.NewLine} TargetSite: {targetsite} {Environment.NewLine} Error: {error}{Environment.NewLine} To: {To}{Environment.NewLine} Subject: {Subject}{Environment.NewLine} Message: {Message} ";
                _applicaitonError.Log(ErrorMessage, string.Empty);
            }

            return response;
        }
    }
}
