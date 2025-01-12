using System.Net.Mail;
using System.Net;

namespace MoviePoint.Utility
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("omartalaat627@gmail.com", "kkms yeyi vosl hwjq")
            };

            return client.SendMailAsync(
                new MailMessage(from: "omartalaat627@gmail.com",
                                to: email,
                                subject,
                                message
                                )
                {
                    IsBodyHtml = true
                });
        }
    }
}

