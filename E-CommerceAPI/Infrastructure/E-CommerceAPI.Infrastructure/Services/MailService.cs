using E_CommerceAPI.Application.Abstractions.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Infrastructure.Services
{
    public class MailService : IMailService
    {
        readonly IConfiguration _configuration;

        public MailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

      
        public async Task SendMailAsync(string to, string subject, string body, bool isBodyHtml = true)
        {
           await SendMailAsync(new[] {to},subject,body,isBodyHtml);

        }

        public async Task SendMailAsync(string[] tos, string subject, string body, bool isBodyHtml = true)
        {
            MailMessage mail = new();
            mail.IsBodyHtml = isBodyHtml;
            foreach (var to in tos)
            {
                mail.To.Add(to);
            }
            mail.Subject = subject;
            mail.Body = body;
            mail.From = new(_configuration["Mail:Username"],"E-Commerce Leo",System.Text.Encoding.UTF8);

            SmtpClient smtp = new();
            smtp.Credentials = new NetworkCredential(_configuration["Mail:Username"], _configuration["Mail:Password"]);
            
            if (int.TryParse(_configuration["Mail:Port"], out int port))
                smtp.Port = port;
            else
                throw new InvalidOperationException("Invalid port value.");
            
            smtp.EnableSsl = true;
            smtp.Host = _configuration["Mail:Host"];
            await smtp.SendMailAsync(mail);

        }

      
        
        public async Task SendPasswordResetMailAsync(string to,string userId,string resetToken)
        {
            StringBuilder mail = new();
            mail.AppendLine("Merhaba<br> Eger yeni sifre Talebinde bulunduysaniz asagidaki linkden sifrenizi yenileye bilirsiz. <br><strong><a target=\"_blank\" href=\"");  //  href=\" icine linki yaziriq string dirnaq goturmur deye bele yaziriq baglayanda bu cure olur \"  href=\"http://localhost:4200/update-password/userId/resetToken\"
            mail.AppendLine(_configuration["AngularClientUrl"]);
            mail.AppendLine("/update-password/");
            mail.AppendLine(userId);
            mail.AppendLine("/");
            mail.AppendLine(resetToken);
            mail.AppendLine("\"> Yeni sifre Talebi icin tiklayiniz... </a> </strong> <br> <br> <span style=\"font-size:12px;\"> Not Egerki bu taleb tarafinizca gerceklesdirilmemise lutfen bu maili ciddiye almayiniz </span> <br> Saygilarimizla...<br><br><br> E-Commerce");

          await   SendMailAsync(to,"Sifre Yenileme Talebi", mail.ToString());
          

        }







        public async Task SendCompletedOrderMailAsync(string to, string orderCode, DateTime orderDate, string userName)
        {
            string mail = $" Hormetli {userName} Salam <br>"
                + $"{orderDate} tarixinde vermis olduqunuz {orderCode} koldu sifarisniz tamlanib,catdirilma ucun carqo firmasina verilmisdir";

            await SendMailAsync(to, $"{orderCode} Sifaris Numarali Sifariniz Tamamlandi", mail);

        }
        
    }
}
