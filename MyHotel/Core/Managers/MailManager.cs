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
        public MailManager()
        {
            // отправитель - устанавливаем адрес и отображаемое в письме имя
            MailAddress from = new MailAddress("RedLeonFire@mail.ru", "Tom");
            // кому отправляем
            MailAddress to = new MailAddress("RedLeonFire@yandex.by");
            // создаем объект сообщения
            MailMessage m = new MailMessage(from, to);
            // тема письма
            m.Subject = "Тест";
            // текст письма
            m.Body = "<h2>Письмо-тест работы smtp-клиента</h2>";
            // письмо представляет код html
            m.IsBodyHtml = true;
            // адрес smtp-сервера и порт, с которого будем отправлять письмо
            SmtpClient smtp = new SmtpClient("smtp.mail.ru", 2525);
            // логин и пароль
            //smtp.Credentials = new NetworkCredential("RedLeonFire@mail.ru", пароль);
            smtp.EnableSsl = true;
            smtp.Send(m);
        }
    }
}
