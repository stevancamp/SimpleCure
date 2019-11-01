using Library.ErrorLogging.Methods;
using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;

namespace Library.Email.Methods
{
    public class EmailMessage
    {       
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
                mail.IsBodyHtml = true;
                smtpServer.Port = Convert.ToInt32(ConfigurationManager.AppSettings["EmailPortNumber"]);
                smtpServer.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["EmailAddress"], ConfigurationManager.AppSettings["EmailPassword"]);
                smtpServer.EnableSsl = true;
                smtpServer.Send(mail);

                response.ResponseSuccess = true;
                response.ResponseMessage = "Email sent successfully";
                response.responseTypes = ResponseTypes.Success;
            }
            catch (Exception ex)
            {
                ApplicationError _applicaitonError = new ApplicationError();
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                string source = ex.Source;
                string stacktrace = ex.StackTrace;
                string targetsite = ex.TargetSite.ToString();
                string error = ex.InnerException?.ToString() ?? ex.ToString();
                string ErrorMessage = $"There was an error at {DateTime.Now} {Environment.NewLine} Method: {methodName} {Environment.NewLine} Source: {source} {Environment.NewLine} StackTrace: {stacktrace} {Environment.NewLine} TargetSite: {targetsite} {Environment.NewLine} Error: {error}{Environment.NewLine} To: {To}{Environment.NewLine} Subject: {Subject}{Environment.NewLine} Message: {Message} ";
                _applicaitonError.Log(ErrorMessage, string.Empty);

                response.ResponseSuccess = false;
                response.ResponseMessage = "Email not sent successfully";
                response.responseTypes = ResponseTypes.Failure;
            }
            return response;
        }

        public ResponseBase SendMessageAttachment(string To, string Subject, string Message, Attachment attachment)
        {
            ResponseBase response = new ResponseBase();
            try
            {
                MailMessage mail = new MailMessage();               
                mail.Subject = Subject;
                mail.From = new MailAddress(ConfigurationManager.AppSettings["EmailAddress"]);
                mail.To.Add(To);
                mail.Body = Message;
                mail.Attachments.Add(attachment);
                using (SmtpClient smtp = new SmtpClient(ConfigurationManager.AppSettings["EmailHost"], 587))
                {
                    smtp.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["EmailAddress"], ConfigurationManager.AppSettings["EmailPassword"]);
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
                response.ResponseSuccess = true;
                response.ResponseMessage = "Email with Attachment sent successfully.";
                response.responseTypes = ResponseTypes.Success;
            }
            catch (Exception ex)
            {
                ApplicationError _applicaitonError = new ApplicationError();
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                string source = ex.Source;
                string stacktrace = ex.StackTrace;
                string targetsite = ex.TargetSite.ToString();
                string error = ex.InnerException?.ToString() ?? ex.ToString();
                string ErrorMessage = $"There was an error at {DateTime.Now} {Environment.NewLine} Method: {methodName} {Environment.NewLine} Source: {source} {Environment.NewLine} StackTrace: {stacktrace} {Environment.NewLine} TargetSite: {targetsite} {Environment.NewLine} Error: {error}{Environment.NewLine} To: {To}{Environment.NewLine} Subject: {Subject}{Environment.NewLine} Message: {Message} ";
                _applicaitonError.Log(ErrorMessage, string.Empty);

                response.ResponseSuccess = false;
                response.ResponseMessage = "Email with attachment not sent successfully";
                response.responseTypes = ResponseTypes.Failure;
            }
            return response;
        }
    }
}
