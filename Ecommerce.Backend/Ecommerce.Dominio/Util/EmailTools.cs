using System.Net;
using System.Net.Mail;

namespace Ecommerce.Dominio.Util
{
    public static class EmailTools
    {
        public static void EnviarEmailResetSenha(string email, string linkDeReset)
        {
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("rpiveto3@gmail.com", "wnbe sfuk pyaz yzak"),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress("rpiveto3@gmail.com"),
                Subject = "Recuperação de Senha",
                Body = $"Clique no link para resetar sua senha: {linkDeReset}",
                IsBodyHtml = true,
            };

            mailMessage.To.Add(email);

            smtpClient.Send(mailMessage);
        }
    }
}
