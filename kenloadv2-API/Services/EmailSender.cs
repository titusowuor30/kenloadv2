using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace KenloadV2API.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailConfiguration _configuration;

        public EmailSender(EmailConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void SendEmail(Message message)
        {
            var emailMessage = CreateEmailMessage(message);
            Send(emailMessage);
        }
        private MimeMessage CreateEmailMessage(Message message)
        {
            var emailMessage=new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(null,_configuration.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject=message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text=string.Format("<h4 style='color:yellow;'>{0}</h4>",message.Body)};

            return emailMessage;
        }

        private void Send(MimeMessage mimeMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect(_configuration.EmailServer, _configuration.Port, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(_configuration.Username, _configuration.Password);
                    client.Send(mimeMessage);
                    Console.WriteLine("Email Sent!");
                }
                catch (Exception ex)
                {
                 Console.WriteLine(ex.Message.ToString());   
                }
                finally{
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }
    }
}