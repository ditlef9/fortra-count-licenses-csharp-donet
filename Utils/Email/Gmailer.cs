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
using FortraCountLicenses.Utils.GCP;

namespace FortraCountLicenses.Utils.Email
{
    public class Gmailer
    {
        private readonly GmailerGoogleServiceAccount _gmailerGoogleServiceAccount;
        private readonly string _emailFrom;

        // Google Gmail packages
        private GmailService? _gmailService;

        // Constructor
        public Gmailer(GmailerGoogleServiceAccount gmailerGoogleServiceAccount, string emailFrom)
        {
            _gmailerGoogleServiceAccount = gmailerGoogleServiceAccount;
            _emailFrom = emailFrom;

            Console.WriteLine($"[Gmailer Constructor] _emailFrom: {_emailFrom}");
            Console.WriteLine($"[Gmailer Constructor] GmailerGoogleServiceAccount.ClientEmail: {_gmailerGoogleServiceAccount.ClientEmail}");

            AuthenticateServiceAccount();
        }

        // Authenticate and initialize Gmail service
        private void AuthenticateServiceAccount()
        {
            Console.WriteLine("[AuthenticateServiceAccount] Starting authentication...");
            var credential = GoogleCredential.FromJson(JsonConvert.SerializeObject(_gmailerGoogleServiceAccount))
                .CreateScoped(GmailService.Scope.GmailSend)
                .CreateWithUser(_emailFrom);

            _gmailService = new GmailService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "FortraCountLicenses"
            });
            Console.WriteLine("[AuthenticateServiceAccount] GmailService has been initialized.");
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
            Console.WriteLine("[SendEmailAsync] Preparing to send email...");
            Console.WriteLine($"[SendEmailAsync] _emailFrom: {_emailFrom}");
            Console.WriteLine($"[SendEmailAsync] emailTo: {emailTo}");
            Console.WriteLine($"[SendEmailAsync] emailSubject: {emailSubject}");

            var message = new MimeMessage();

            // Use an empty display name if you don't have one
            Console.WriteLine($"[SendEmailAsync] Adding From address: {_emailFrom}");
            message.From.Add(new MailboxAddress(string.Empty, _emailFrom));

            // Split comma-separated email addresses and add each one
            var emailAddresses = emailTo.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var addr in emailAddresses)
            {
                var trimmedAddress = addr.Trim();
                Console.WriteLine($"[SendEmailAsync] Adding To address: {trimmedAddress}");
                message.To.Add(new MailboxAddress(string.Empty, trimmedAddress));
            }

            message.Subject = emailSubject;

            var bodyBuilder = new BodyBuilder { HtmlBody = emailContent };

            if (!string.IsNullOrEmpty(emailReplyTo))
            {
                Console.WriteLine($"[SendEmailAsync] Adding Reply-To header: {emailReplyTo}");
                message.Headers.Add("Reply-To", emailReplyTo);
            }

            // Add attachment if available
            if (!string.IsNullOrEmpty(attachmentFileName) && attachmentBytes != null && !string.IsNullOrEmpty(attachmentMimeType))
            {
                Console.WriteLine($"[SendEmailAsync] Attaching file: {attachmentFileName} with MIME type: {attachmentMimeType}");
                bodyBuilder.Attachments.Add(attachmentFileName, attachmentBytes, ContentType.Parse(attachmentMimeType));
            }

            message.Body = bodyBuilder.ToMessageBody();
            Console.WriteLine("[SendEmailAsync] Email message constructed successfully.");

            using var memoryStream = new MemoryStream();
            await message.WriteToAsync(memoryStream);
            string rawMessage = Convert.ToBase64String(memoryStream.ToArray())
                .Replace('+', '-')
                .Replace('/', '_')
                .Replace("=", "");

            Console.WriteLine("[SendEmailAsync] Raw message prepared for sending.");

            var gmailMessage = new Message { Raw = rawMessage };

            if (_gmailService == null)
            {
                throw new InvalidOperationException("Gmail service is not initialized.");
            }

            try
            {
                Console.WriteLine("[SendEmailAsync] Sending email...");
                var request = _gmailService.Users.Messages.Send(gmailMessage, "me");
                var response = await request.ExecuteAsync();
                Console.WriteLine($"[SendEmailAsync] Email sent successfully, Message ID: {response.Id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[SendEmailAsync] Error sending email: {ex.Message}");
                throw new Exception($"Error sending email: {ex.Message}");
            }
        }

    }
}
