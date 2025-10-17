using MailKit.Net.Smtp;
using MimeKit;
using MailKit.Security;

namespace OnlineFoodDelivery.EmailNotificationsService
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var senderEmail = _config["EmailSettings:SenderEmail"];
            var senderPassword = _config["EmailSettings:Password"];

            Console.WriteLine($"Preparing to send email to: {toEmail}");

            var email = new MimeMessage();
            email.From.Add(new MailboxAddress("DeliveryApp", senderEmail));
            email.To.Add(new MailboxAddress("", toEmail));
            email.Subject = subject;
            email.Body = new TextPart("plain") { Text = body };

            using var smtp = new SmtpClient();
            //await smtp.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
            await smtp.ConnectAsync("smtp.gmail.com", 465, SecureSocketOptions.SslOnConnect);

            await smtp.AuthenticateAsync(senderEmail, senderPassword);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);

            Console.WriteLine("Email sent successfully.");
        }
    }
}

















//using MailKit.Net.Smtp;
//using MimeKit;

//namespace DeliveryManagement.EmailNotificationsService
//{
//    public class EmailService : IEmailService
//    {
//        private readonly IConfiguration _config;

//        public EmailService(IConfiguration config)
//        {
//            _config = config;
//        }

//        public async Task SendEmailAsync(string toEmail, string subject, string body)
//        {
//            var senderEmail = _config["EmailSettings:SenderEmail"];
//            var senderPassword = _config["EmailSettings:Password"];

//            var email = new MimeMessage();
//            email.From.Add(new MailboxAddress("DeliveryApp", senderEmail));
//            email.To.Add(new MailboxAddress("", toEmail));
//            email.Subject = subject;
//            email.Body = new TextPart("plain") { Text = body };

//            using var smtp = new SmtpClient();
//            await smtp.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
//            await smtp.AuthenticateAsync(senderEmail, senderPassword);
//            await smtp.SendAsync(email);
//            await smtp.DisconnectAsync(true);
//        }

//    }
//}






















//        //public async Task SendEmailAsync(string toEmail, string subject, string body)
//        //{
//        //    var email = new MimeMessage();
//        //    email.From.Add(new MailboxAddress("DeliveryApp", _config["EmailSettings:SenderEmail"]));
//        //    email.To.Add(new MailboxAddress("", toEmail));
//        //    email.Subject = subject;
//        //    email.Body = new TextPart("plain") { Text = body };

//        //    using var smtp = new SmtpClient();
//        //    //await smtp.ConnectAsync(_config["EmailSettings:SmtpServer"], int.Parse(_config["EmailSettings:Port"]), false);
//        //    //await smtp.AuthenticateAsync(_config["EmailSettings:SenderEmail"], _config["EmailSettings:Password"]);

//        //    await smtp.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
//        //    await smtp.AuthenticateAsync(senderEmail, senderPassword);

//        //    await smtp.SendAsync(email);
//        //    await smtp.DisconnectAsync(true);
//        //}