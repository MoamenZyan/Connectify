using Connectify.Application.Interfaces.ExternalNotificationsInterfaces.EmailStrategies;
using SendGrid.Helpers.Mail;
using SendGrid;
using Connectify.Infrastructure.Configurations.ExternalNotificationsConfigurations;
using Microsoft.Extensions.Options;

namespace Connectify.Infrastructure.Services.ExternalNotificationsServices.EmailStrategies
{
    public class WelcomeEmailStrategy : IWelcomeEmailStrategy
    {
        private readonly EmailServiceConfiguration _emailServiceConfiguration;
        public WelcomeEmailStrategy(IOptions<EmailServiceConfiguration> emailServiceConfiguration)
        {
            _emailServiceConfiguration = emailServiceConfiguration.Value;
        }
        public async Task Send(string userName, string to, Dictionary<string, string> data = null!)
        {
            var client = new SendGridClient(_emailServiceConfiguration.ApiKey);
            var from = new EmailAddress(_emailServiceConfiguration.FromEmail, "Connectify");
            var subject = "Welcome On Board!";
            var toUser = new EmailAddress(to, userName);
            var htmlContent = @$"
                            <h1>Welcome To Connectify</h1>
                            <p>{userName}, Welcome on your platform connectify!</p>
                            <p>Where you can speak, chat, interact with users !</p>
                            <strong>Don't forget to verify your account</strong>
                ";
            var msg = MailHelper.CreateSingleEmail(from, toUser, subject, "", htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
