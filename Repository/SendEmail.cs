using Azure;
using Azure.Communication.Email;
using MealMasterAPI.Data;
using MealMasterAPI.Repository.IRepository;
using System.Net.Mail;

namespace MealMasterAPI.Repository
{
    public class SendEmail : ISendEmail

    {
        private EmailClient _emailClient;
        private readonly AppDbContext _bd;

        public SendEmail(AppDbContext bd)
        {
            _bd = bd;
            _emailClient = new EmailClient(Environment.GetEnvironmentVariable("KEY_API"));
        }

        public void SendPasswordResetEmail(string email, string resetLink)
        {

            EmailSendOperation emailSendOperation = _emailClient.Send(
                WaitUntil.Completed,
                senderAddress: "DoNotReply@0956d554-7718-46a5-9e08-1780a9e31fc1.azurecomm.net",
                recipientAddress: email,
                subject: "Password Reset",
                htmlContent: $"<html><body><h2>Restablecimiento de contraseña</h2><p>Hola,</p><p>Hemos recibido una solicitud para restablecer la contraseña de tu cuenta.</p><p>Por favor, haz clic en el siguiente enlace para restablecer tu contraseña:</p><p><a href=\"{resetLink}\">Click aquí para restablecer tu contraseña</a></p><p>Si no solicitaste este cambio, por favor ignora este correo.</p><p>Gracias,</p><p>Tu equipo de soporte</p></body></html>", 
                plainTextContent: $"Click the link to reset your password: {resetLink}");

        }
    }
}
