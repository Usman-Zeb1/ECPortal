using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Threading.Tasks;

namespace Pk.Com.Jazz.ECP.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailSettings _emailSettings;
        private readonly IWebHostEnvironment _env;

        public EmailSender(
            IOptions<EmailSettings> emailSettings,
            IWebHostEnvironment env)
        {
            _emailSettings = emailSettings.Value;
            _env = env;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            try
            {
                var mimeMessage = new MimeMessage();

                mimeMessage.From.Add(new MailboxAddress(_emailSettings.SenderName, _emailSettings.Sender));

                mimeMessage.To.Add(new MailboxAddress("", email)); // No display name                                                                   // Corrected line: Add recipient's email address

                mimeMessage.Subject = subject;

                mimeMessage.Body = new TextPart("html")
                {
                    Text = message
                };

                using (var client = new SmtpClient())
                {
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true; // accept all SSL certificates (in case the server supports STARTTLS)

                    if (_env.IsDevelopment())
                    {
                        await client.ConnectAsync(_emailSettings.MailServerA, _emailSettings.MailPort, false); // The third parameter is useSSL (true if the client should make an SSL-wrapped connection to the server; otherwise, false).

                        if (!client.IsConnected)
                        {
                            await client.ConnectAsync(_emailSettings.MailServerB, _emailSettings.MailPort, false);
                        }
                    }
                    else
                    {
                        await client.ConnectAsync(_emailSettings.MailServerA);

                        if (!client.IsConnected)
                        {
                            await client.ConnectAsync(_emailSettings.MailServerB);
                        }
                    }

                    //await client.AuthenticateAsync(_emailSettings.Sender, _emailSettings.Password); // Note: only needed if the SMTP server requires authentication

                    await client.SendAsync(mimeMessage);

                    await client.DisconnectAsync(true);
                }

            }
            catch (Exception ex)
            {
                // TODO: handle exception
                throw new InvalidOperationException(ex.Message);
            }
        }


    }
}
