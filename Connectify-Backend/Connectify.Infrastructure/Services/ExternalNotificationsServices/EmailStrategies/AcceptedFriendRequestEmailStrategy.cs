using Connectify.Application.Interfaces.ExternalNotificationsInterfaces.EmailStrategies;
using Connectify.Infrastructure.Configurations.ExternalNotificationsConfigurations;
using Microsoft.Extensions.Options;
using SendGrid.Helpers.Mail;
using SendGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connectify.Infrastructure.Services.ExternalNotificationsServices.EmailStrategies
{
    public class FriendRequestAcceptedEmailStrategy : IFriendRequestAcceptedEmailStrategy
    {
        private readonly EmailServiceConfiguration _emailServiceConfiguration;
        public FriendRequestAcceptedEmailStrategy(IOptions<EmailServiceConfiguration> emailServiceConfiguration)
        {
            _emailServiceConfiguration = emailServiceConfiguration.Value;
        }
        public async Task Send(string userName, string to, Dictionary<string, string> data = null!)
        {
            var client = new SendGridClient(_emailServiceConfiguration.ApiKey);
            var from = new EmailAddress(_emailServiceConfiguration.FromEmail, "Connectify");
            var subject = "Friend request accepted!";
            var toUser = new EmailAddress(to, userName);
            var htmlContent = @$"
                            <h1>Your friend request got accepted!</h1>
                            <p>{data["SenderName"]}, has accept your friend request!</p>
                            <p>You can now chat each other!</p>
                ";
            var msg = MailHelper.CreateSingleEmail(from, toUser, subject, "", htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
