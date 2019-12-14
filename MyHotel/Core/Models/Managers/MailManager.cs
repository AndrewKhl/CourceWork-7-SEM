using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MyHotel.Core
{
    public class MailManager
    {
        private const string AdminEmail = "RedLeonFire@mail.ru";
        private readonly MailAddress _admin;

        public MailManager()
        {
            _admin = new MailAddress(AdminEmail, "Hotel");
        }

        public void SendRegistration(Person person)
        {

        }

        private void SendMail(string emailTo, string header, string text)
        {
            MailAddress to = new MailAddress(emailTo);

            MailMessage mail = new MailMessage(_admin, to);

            mail.Subject = header;
            mail.Body = $"<h2>{text}</h2>";
            mail.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient("smtp.mail.ru", 2525);
            smtp.Credentials = new NetworkCredential(AdminEmail, "LxHWwSKL");
            smtp.EnableSsl = true;
            smtp.Send(mail);
        }
    }
}
