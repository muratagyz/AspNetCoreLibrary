using SendGrid.Helpers.Mail;
using SendGrid;

namespace Hangfire.Web.Services;

public class EmailSender : IEmailSender
{
    private readonly IConfiguration _config;

    public EmailSender(IConfiguration config)
    {
        _config = config;
    }

    public async Task Sender(string userId, string message)
    {
        var apiKey = _config.GetSection("APIs")["SendGridApi"];
        var client = new SendGridClient(apiKey);
        var from = new EmailAddress("muratagyuz@outlook.com", "Example User");
        var subject = "www.muratagyuz.com bilgilendirme";
        var to = new EmailAddress("mrt.agyzz.00@outlook.com", "Example User");
        var plainTextContent = $"{message}";
        var htmlContent = $"<strong> {message} </strong>";
        var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
        var response = await client.SendEmailAsync(msg);
    }
}