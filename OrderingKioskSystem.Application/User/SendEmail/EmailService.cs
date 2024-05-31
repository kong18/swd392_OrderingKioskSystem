using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.User.SendEmail
{
    public interface IEmailService
    {
        void SendMail(SendMailModel model);
    }

    public class EmailService : IEmailService
    {

        public EmailService()
        {

        }

        public void SendMail(SendMailModel model)
        {
            try
            {
                MailMessage mailMessage = new MailMessage()
                {
                    Subject = "Password of OrderingKioskSystem",
                    Body = model.Content ,
                    IsBodyHtml = false,
                };
                mailMessage.From = new MailAddress(EmailSettingModel.Instance.FromEmailAddress, EmailSettingModel.Instance.FromDisplayName);
                mailMessage.To.Add(model.ReceiveAddress);

                var smtp = new SmtpClient()
                {
                    EnableSsl = EmailSettingModel.Instance.Smtp.EnableSsl,
                    Host = EmailSettingModel.Instance.Smtp.Host,
                    Port = EmailSettingModel.Instance.Smtp.Port,
                };
                var network = new NetworkCredential(EmailSettingModel.Instance.Smtp.EmailAddress, EmailSettingModel.Instance.Smtp.Password);
                smtp.Credentials = network;

                smtp.Send(mailMessage);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }

    public class VerificationCodeGenerator
    {
        private static readonly RandomNumberGenerator _rng = new RNGCryptoServiceProvider();

        public static string Code = "";

        public static int GenerateVerificationCode()
        {
            byte[] buffer = new byte[4];
            _rng.GetBytes(buffer);
            int randomCode = BitConverter.ToInt32(buffer, 0) & int.MaxValue;
            return randomCode % 900000 + 100000; // Ensure it's a 6-digit number
        }
    }

    public class SendMailModel
    {
        public string ReceiveAddress { get; set; }
        public string Content { get; set; }
    }
    public class EmailSettingModel
    {
        public static EmailSettingModel Instance { get; set; }
        public string FromEmailAddress { get; set; }
        public string FromDisplayName { get; set; }
        public Smtp Smtp { get; set; }
    }

    public class Smtp
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public bool EnableSsl { get; set; }
        public bool UseCredential { get; set; }
    }
}
