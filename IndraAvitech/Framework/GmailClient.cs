using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SendEmailProject.Framework
{
    public class GmailClient
    {
        private readonly ClientSecrets _secrets;

        public GmailClient(string gmailClientId, string gmailClientSecret)
        {
            _secrets = new ClientSecrets()
            {
                ClientId = gmailClientId,
                ClientSecret = gmailClientSecret
            };
        }

        private async Task<UserCredential> LoginAsync(string[] scopes)
        {
            UserCredential credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                _secrets,
                scopes,
                "user",
                CancellationToken.None);

            return credential;
        }

        private Message GetLastMessage(string userId)
        {
            try
            {
                string[] scopes = new[] { GmailService.Scope.GmailReadonly };
                UserCredential credential = LoginAsync(scopes).Result;

                using var gmailService = new GmailService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential
                });

                var request = gmailService.Users.Messages.List(userId);
                request.MaxResults = 1; // Retrieve only the last message
                var response = request.Execute();

                if (response?.Messages != null && response.Messages.Any())
                {
                    string messageId = response.Messages.First().Id;
                    var messageRequest = gmailService.Users.Messages.Get(userId, messageId);
                    return messageRequest.Execute();
                }
                else
                {
                    Console.WriteLine("No messages found.");
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: " + e.Message);
                return null;
            }
        }

        public void CheckThatEmailWasReceived(string emailFrom, string emailSubject, TimeSpan timeout)
        {
            DateTime startTime = DateTime.Now;
            string subject = null;
            string sender = null;

            while ((subject != emailSubject || sender != emailFrom) && DateTime.Now - startTime < timeout)
            {
                Message message = GetLastMessage("me");

                if (message?.Payload?.Headers != null)
                {
                    foreach (var header in message.Payload.Headers)
                    {
                        if (header.Name == "Subject")
                        {
                            subject = header.Value;
                        }
                        else if (header.Name == "From")
                        {
                            sender = header.Value;
                        }
                    }
                }

                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
            }

            if (subject == emailSubject && sender == emailFrom)
            {
                return; // Email received successfully.
            }

            throw new TimeoutException($"Email from {emailFrom} with subject '{emailSubject}' not received within the timeout.");
        }
    }
}