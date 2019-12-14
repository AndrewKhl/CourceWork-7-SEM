using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MyHotel.Core
{
    public class MailManager : DbContext
    {
        private const string AdminEmail = "RedLeonFire@mail.ru";
        private readonly MailAddress _admin;

        public MailManager() : base("DbConnection")
        {
            _admin = new MailAddress(AdminEmail, "Hotel");

        }

        public DbSet<MailReport> Reports { get; set; }

        public void SendRegistrarion(Person person)
        {
            CreateText(person, $"Thank you for registering in our application. We send you your login: {person.Email} and password: {person.Password}.", true);
        }

        public void SendReservation(Person person, HousingOrder order)
        {
            CreateText(person, $"Thank you for choosing our hotel. Date of your arrival: {order.InTime}-{order.OutTime}. Cost of living: {order.Cost}$. {(order.IsPaid ? "Payment has been made." : "")}", false);
        }

        private void CreateText(Person person, string text, bool isRegistration)
        {
            string message = $"<table width=\"100 % \" border=\"0\" cellspacing=\"0\" cellpadding=\"0\"><tr><td align = \"center\" valign = \"top\" bgcolor = \"#f6f3e4\" style = \"background-color:#f6f3e4;\"><br> <br> <table width = \"600\" border = \"0\" cellspacing = \"0\" cellpadding = \"0\"> <tr> <td align = \"center\" valign = \"top\" style = \"padding-left:13px; padding-right:13px; background-color:#ffffff;\"><table width = \"100%\" border = \"0\" cellspacing = \"0\" cellpadding = \"0\"> <tr> <td><table width = \"84\" border = \"0\" cellspacing = \"0\" cellpadding = \"0\"> <tr> <td height = \"80\" align = \"center\" valign = \"middle\" bgcolor = \"#d80000\" style = \"font-family:Arial, Helvetica, sans-serif; color:#ffffff;\"><div style = \"font-size:15px;\"><b> Date </b></div><div style = \"font-size:30px;\"><b> {DateTime.Now.Day} </b></div></td> </tr> </table></td> </tr> <tr> <td align = \"center\" valign = \"middle\" style = \"font-family:Georgia, 'Times New Roman', Times, serif; font-size:48px;\"><i> {(isRegistration ? "Registration" : "Payment")} </i></td> </tr> <tr> <td align = \"center\" valign = \"middle\" style = \"padding-top:7px;\"><table width = \"240\" border = \"0\" cellspacing = \"0\" cellpadding = \"0\"> <tr> <td height = \"31\" align = \"center\" valign = \"middle\" bgcolor = \"#d80000\" style = \"font-family:Georgia, 'Times New Roman', Times, serif; font-size:19px; color:#ffffff;\"> {DateTime.Now.Month} / {DateTime.Now.Year} </td> </tr> </table></td> </tr> <tr> <td align = \"center\" valign = \"middle\" style = \"padding-top:15px;\"><img src = \"https://avatars.mds.yandex.net/get-altay/247136/2a0000015b2e90b822caafa7c6a73c545102/XXL\" width = \"573\" height = \"400\" style = \"display:block;\"></td> </tr> <tr> <td align = \"center\" valign = \"middle\" style = \"padding-bottom:15px; padding-top:15px;\"><img src = \"http://www.everybodyhairandbeauty.co.uk/communities/5/004/012/677/845/images/4610551681_966x142.png\" width = \"573\"></td> </tr> <tr> <td align = \"center\" valign = \"middle\" style = \"font-family:Georgia, 'Times New Roman', Times, serif; color:#000000; font-size:24px; padding-bottom:5px;\"><i> Dear {person.Name}, </i> <tr> <td align = \"left\" valign = \"middle\" style = \"font-family:Georgia, 'Times New Roman', Times, serif; color:#000000; font-size:15px;\"> {text}</div> <tr> <td align = \"center\" valign = \"middle\" style = \"padding-bottom:15px; padding-top:15px;\"><img src = \"http://www.everybodyhairandbeauty.co.uk/communities/5/004/012/677/845/images/4610551681_966x142.png\" width = \"573\"></td> </tr> <tr> <td align = \"left\" valign = \"middle\" style = \"font-family:Georgia, 'Times New Roman', Times, serif; font-size:12px; color:#000000;\"> <div style = \"color:#b30467; font-size:15px;\"><b> Contact Us </b></div> <div><br> This letter was sent as part of the implementation of a cursive project, please do not try to contact this address<br> <br> </div>><div><br> The project was made by:<br> Andrew Khloptsau, <br> Palina Shymanouskaya,<br> Group: 653503 <br> <br> </div></td> </tr> </table></td> </tr> </table> <br> <br></td> </tr> <tr> <td align = \"center\" valign = \"top\"> &nbsp;</td> </tr> </table> ";
            SendMail(person.Email, isRegistration ? "Registration" : "Payment", message);

            Reports.Add(new MailReport()
            {
                From = person.Email,
                Date = DateTime.Now.ToString(),
                Type = isRegistration ? "Registration" : "Reservation"
            });

            SaveChangesAsync();
        }

        private void SendMail(string emailTo, string header, string text)
        {
            MailAddress to = new MailAddress(emailTo);

            MailMessage mail = new MailMessage(_admin, to);

            mail.Subject = header;
            mail.Body = text;
            mail.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient("smtp.mail.ru", 2525)
            {
                Credentials = new NetworkCredential(AdminEmail, "LxHWwSKL"),
                EnableSsl = true
            };
            smtp.Send(mail);
        }
    }
}
