using Company.G03.DAL.Models;
using System.Net;
using System.Net.Mail;

namespace Company.G03.PL.Helper
{
    public static class EmailSettings
    {
        public static void SendEmail(Email email)
        {
            // Mail Server : gmail.com

            // Smpt

            var client = new SmtpClient("smtp.gmail.com", port: 587 );
            client.EnableSsl = true;

            client.Credentials = new NetworkCredential(userName: "routec41v02@gmail.com", password:"//hjgghgygtduhkxo");

            client.Send("routec41v02@gmail.com", email.To, email.Subject, email.Body);
              
        
        }
    }
}
