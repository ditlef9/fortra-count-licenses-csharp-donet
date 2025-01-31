using System;
using System.IO;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Newtonsoft.Json;
using MimeKit;

namespace FortraCountLicenses.Utils.Email
{
    public class Gmailer
    {
        private readonly string _projectId;
        private readonly string _secretId;
        private readonly string _emailFrom;
        private dynamic _serviceAccountJson;
        private GmailService _gmailService;

        // Constructor
        public Gmailer(string serviceAccountJson, string emailFrom)
        {
            _serviceAccountJson = serviceAccountJson;
            _emailFrom = emailFrom;

            AuthenticateServiceAccount();
        }

        // Authenticate and initialize Gmail service
        private void AuthenticateServiceAccount()
        {
            var credential = GoogleCredential.FromJson(JsonConvert.SerializeObject(_serviceAccountJson))
                .CreateScoped(GmailService.Scope.GmailSend)
                .CreateWithUser(_emailFrom);

            _gmailService = new GmailService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "FortraCountLicenses"
            });
        }

        // Send Email
        public async Task SendEmailAsync(
            string emailTo,
            string emailSubject,
            string emailContent,
            string? emailReplyTo = null,
            string? attachmentFileName = null,
            byte[]? attachmentBytes = null,
            string? attachmentMimeType = null)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_emailFrom, _emailFrom));
            message.To.Add(new MailboxAddress(emailTo, emailTo));
            message.Subject = emailSubject;

            var bodyBuilder = new BodyBuilder { HtmlBody = emailContent };

            if (!string.IsNullOrEmpty(emailReplyTo))
            {
                message.Headers.Add("Reply-To", emailReplyTo);
            }

            // Add attachment if available
            if (!string.IsNullOrEmpty(attachmentFileName) && attachmentBytes != null && !string.IsNullOrEmpty(attachmentMimeType))
            {
                bodyBuilder.Attachments.Add(attachmentFileName, attachmentBytes, ContentType.Parse(attachmentMimeType));
            }

            message.Body = bodyBuilder.ToMessageBody();

            using var memoryStream = new MemoryStream();
            await message.WriteToAsync(memoryStream);
            string rawMessage = Convert.ToBase64String(memoryStream.ToArray())
                .Replace('+', '-')
                .Replace('/', '_')
                .Replace("=", "");

            var gmailMessage = new Message { Raw = rawMessage };

            try
            {
                var request = _gmailService.Users.Messages.Send(gmailMessage, "me");
                var response = await request.ExecuteAsync();
                Console.WriteLine($"Email sent successfully to {emailTo}, Message ID: {response.Id}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error sending email: {ex.Message}");
            }
        }
    }
}
