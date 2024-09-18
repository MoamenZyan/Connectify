using Connectify.Application.Interfaces.ExternalNotificationsInterfaces.EmailStrategies;
using Connectify.Infrastructure.Configurations.ExternalNotificationsConfigurations;
using SendGrid.Helpers.Mail;
using SendGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace Connectify.Infrastructure.Services.ExternalNotificationsServices.EmailStrategies
{
    public class ReceivedFriendRequestEmailStrategy : IReceivedFriendRequestEmailStrategy
    {
        private readonly EmailServiceConfiguration _emailServiceConfiguration;
        public ReceivedFriendRequestEmailStrategy(IOptions<EmailServiceConfiguration> emailServiceConfiguration)
        {
            _emailServiceConfiguration = emailServiceConfiguration.Value;
        }
        public async Task Send(string userName, string to, Dictionary<string, string> data = null!)
        {
            var client = new SendGridClient(_emailServiceConfiguration.ApiKey);
            var from = new EmailAddress(_emailServiceConfiguration.FromEmail, "Connectify");
            var subject = "Received friend request";
            var toUser = new EmailAddress(to, userName);
            var htmlContent = @$"
                            <h1>Received Friend Request!</h1>
                            <p>{userName}, you received friend request from {data["SenderName"]}</p>
                ";
            var msg = MailHelper.CreateSingleEmail(from, toUser, subject, "", htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
