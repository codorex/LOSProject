using System.Net;
using System.Net.Mail;

namespace LOS.EmailClient
{
    public static class CustomEmailClient
    {
        private static MailAddress fromAddress = new MailAddress("longboardonlinestore@gmail.com");
        private static string fromPassword = "123admin";

        public static void SendEmail(MailAddress addressToSendTo, string subject, string body)
        {
            SmtpClient smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };

            using (var message = new MailMessage(fromAddress, addressToSendTo)
            {
                Subject = subject,
                Body = body
            })

                smtp.Send(message);
        }
    }
}
