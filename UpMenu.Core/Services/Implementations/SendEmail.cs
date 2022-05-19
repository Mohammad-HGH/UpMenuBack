using System.Net.Mail;
using UpMenu.Core.Services.Interfaces;

namespace UpMenu.Core.Services.Implementations
{
    public class SendEmail : IMailSender
    {
        public void Send(string to, string subject, string body)
        {
            var defaultEmail = "adsalnet@gmail.com";

            var mail = new MailMessage();

            var SmtpServer = new SmtpClient();

            mail.From = new MailAddress(defaultEmail, "فروشگاه انگولار");
            SmtpServer.Host = "smtp.gmail.com";
            mail.To.Add(to);

            mail.Subject = subject;

            mail.Body = body;

            mail.IsBodyHtml = true;

            //System.Net.Mail.Attachment attachment;
            //attachment = new System.Net.Mail.Attachment("c:/textfile.txt");
            //mail.Attachments.Add(attachment);

            SmtpServer.Port = 587;

            SmtpServer.Credentials = new System.Net.NetworkCredential(defaultEmail, "رمز ایمیل");

            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);
        }
    }
}
