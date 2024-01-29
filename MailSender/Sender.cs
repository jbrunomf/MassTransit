using SendGrid.Helpers.Mail;
using SendGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSender
{
    public static class Sender
    {
        public static void Main()
        {
            Execute().Wait();
        }


        static async Task Execute()
        {
            //var apiKey = Environment.GetEnvironmentVariable("SENDGRID_KEY");
            var apiKey = "";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("contato@jbsoft.io", "JB Tech Challenge");
            var subject = "Novo Pedido Criado";
            var to = new EmailAddress("email@cliente.com", "Nome Cliente");
            var plainTextContent = "";
            var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);

            Console.WriteLine("");
        }
    }
}
