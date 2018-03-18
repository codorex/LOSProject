using System.Net;
using System.Net.Mail;

namespace LOS.EmailClient
{
    class CustomEmailClient
    {
        private readonly MailAddress fromAddress = new MailAddress("longboardonlinestore@gmail.com");
        private readonly string fromPassword = "123admin";

        public void SendEmail(MailAddress addressToSendTo, string subject, string body)
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
