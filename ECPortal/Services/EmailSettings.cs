#nullable disable
using System.ComponentModel.DataAnnotations;

namespace Pk.Com.Jazz.ECP.Services
{
    public class EmailSettings
    {
        public string MailServerA { get; set; }
        public string MailServerB { get; set; }
        public int MailPort { get; set; }
        public string SenderName { get; set; }
        public string Sender { get; set; }
        public string Password { get; set; }

        [Display(Name = "SMTP", ShortName = "Email")]
        public string DisplayName
        {
            get { return $"{MailServerA}-{Sender}"; }
        }
    }
}
