/* The MIT License (MIT)
Copyright © 2014 Ian Cooper <ian_hammond_cooper@yahoo.co.uk>

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the “Software”), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE. */

using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using SendGrid;
using Tasks.Model;
using Tasks.Ports;

namespace Tasks.Adapters.MailGateway
{
    public class MailGateway : IAmAMailGateway
    {
        private readonly IAmAMailTranslator _translator;

        public MailGateway(IAmAMailTranslator translator)
        {
            _translator = translator;
        }

        public void Send(TaskReminder reminder)
        {
            var mail = _translator.Translate(reminder);

            var credentials = new NetworkCredential(
                ConfigurationManager.AppSettings["sendGridUserName"],
                ConfigurationManager.AppSettings["sendGridPassword"]
                );

            //var api = Web.GetInstance(credentials);
            SendReminder(mail);
            //api.DeliverAsync(mail).Wait();
        }

        private void SendReminder(Mail email)
        {
            SmtpClient client = new SmtpClient();
            string supportEmailAddress = Globals.SenderEmail;
            string supportEmailPassword = Globals.SenderPassword;

            client.Credentials = new System.Net.NetworkCredential(supportEmailAddress, supportEmailPassword);
            client.Port = Globals.SenderSMTPPort;
            client.Host = Globals.SenderSMTPHost;
            client.EnableSsl = true;

            MailMessage mailMessage = new MailMessage();

            mailMessage.From = new MailAddress(supportEmailAddress, "Task Master");
            mailMessage.Subject = email.Subject;
            mailMessage.Body = email.Html;

            mailMessage.BodyEncoding = System.Text.Encoding.ASCII;
            mailMessage.IsBodyHtml = true;
            mailMessage.Priority = MailPriority.High;

            mailMessage.To.Clear();
            email.To.ToList().ForEach((mailAddress) =>
            {
                mailMessage.To.Add(new MailAddress(mailAddress.Address, mailAddress.DisplayName));
            });

            client.Send(mailMessage);
        }
    }
}