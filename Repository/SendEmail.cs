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
                htmlContent: $"<a href=\"{resetLink}\">Click here to reset your password</a>",
                plainTextContent: $"Click the link to reset your password: {resetLink}");

        }
    }
}
