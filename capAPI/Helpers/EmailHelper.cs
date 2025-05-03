using System.Net.Mail;
using SendGrid.Helpers.Mail;
using SendGrid;

namespace capAPI.Helpers
{
    public static class EmailHelper
    {
        public  static async Task SendOtpEmail(string userEmail, string title, string message, string otpCode)
        {
            string apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
            if (string.IsNullOrEmpty(apiKey))
            {
                throw new Exception("SendGrid API key is missing from environment variables.");
            }

            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("razanqashou777@gmail.com", "Capstone");
            var to = new EmailAddress(userEmail);
            var subject = title;
            var plainTextContent = $"Dear User, {message} Please use the following OTP code: {otpCode}";

            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, null);
            var response = await client.SendEmailAsync(msg);

            if ((int)response.StatusCode >= 400)
            {
                var responseBody = await response.Body.ReadAsStringAsync();
                throw new Exception($"SendGrid Error: {response.StatusCode} - {responseBody}");
            }
        }


    }
}
