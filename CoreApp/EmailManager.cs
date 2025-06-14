using DTOs;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreApp
{
    public class EmailManager : BaseManager
    {
        
        private readonly string fromEmail = "fabiolazu2000@gmail.com";
        private readonly string fromName = "CenfoCinemas";

        public async Task SendWelcomeEmail(string email, string name)
        {
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(fromEmail, fromName);
            var to = new EmailAddress(email, name);
            var subject = "¡Bienvenido a CenfoCinemas!";
            var plainTextContent = $"Hola {name},\n\nGracias por registrarte en CenfoCinemas.";
            var htmlContent = $"<strong>Hola {name},</strong><br><br>Gracias por registrarte en <b>CenfoCinemas</b>. ¡Nos alegra tenerte a bordo!";

            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg).ConfigureAwait(false);
        }

        public async Task SendNewMovie(string movieTitle, List<User> recipients)
        {
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(fromEmail, fromName);
            var subject = $"Nueva película agregada: {movieTitle}";
            var plainTextContent = $"¡Hola! Hemos agregado una nueva película: {movieTitle}. ¡No te la pierdas!";
            var htmlContent = $"<strong>¡Hola!</strong><br><br> Hemos agregado una nueva película: <b>{movieTitle}</b>.<br>¡Corre a verla en <b>CenfoCinemas</b>!";

            foreach (var user in recipients)
            {
                var to = new EmailAddress(user.Email, user.Name ?? "Usuario");
                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                var response = await client.SendEmailAsync(msg);
            }
        }
    }

}
