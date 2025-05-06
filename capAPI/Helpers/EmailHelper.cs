using System.Net.Mail;
using SendGrid.Helpers.Mail;
using SendGrid;

namespace capAPI.Helpers
{
    public static class EmailHelper
    {
        private static string _apiKey;
        private static string _fromEmail;
        private static string _fromName;

        
        public static void Init(IConfiguration configuration)
        {
            _apiKey = configuration["SendGrid:ApiKey"];
            _fromEmail = configuration["SendGrid:FromEmail"];
            _fromName = configuration["SendGrid:FromName"];

            if (string.IsNullOrWhiteSpace(_apiKey))
                throw new Exception("SendGrid API key is missing in configuration.");
        }

        public static async Task SendOtpEmail(
            string userEmail,
            string subject,
            string message,
            string otpCode)
        {
            var client = new SendGridClient(_apiKey);
            var from = new EmailAddress(_fromEmail, _fromName);
            var to = new EmailAddress(userEmail);
            var plain = $"Dear User, {message} Your code: {otpCode}";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plain, null);

            var response = await client.SendEmailAsync(msg);
            if ((int)response.StatusCode >= 400)
            {
                var body = await response.Body.ReadAsStringAsync();
                throw new Exception($"SendGrid Error: {response.StatusCode} – {body}");
            }
        }

    }
}
