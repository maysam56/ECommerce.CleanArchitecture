using ECommerce.Application.Interfaces.IServices;
using ECommerce.Application.Models;
using ECommerce.Application.Settings;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace ECommerce.Infrastructure.Email;

public class EmailService : IEmailService
{
    private readonly EmailSettings _emailSettings;

    public EmailService(IOptions<EmailSettings> emailSettings)
    {
        _emailSettings = emailSettings.Value;
    }

    public async Task SendEmailAsync(
        string to,
        string subject,
        string body,
        List<EmailAttachment>? attachments = null)
    {
        using var smtpClient = new SmtpClient(_emailSettings.Host)
        {
            Port = _emailSettings.Port,
            EnableSsl = true,
            Credentials = new NetworkCredential(
                _emailSettings.Username,
                _emailSettings.Password)
        };

        using var mailMessage = new MailMessage
        {
            From = new MailAddress(_emailSettings.Username),
            Subject = subject,
            Body = body,
            IsBodyHtml = true
        };

        mailMessage.To.Add(to);
    
        if (attachments is not null)
        {
            foreach (var attachment in attachments)
            {
                var stream = new MemoryStream(attachment.Content);

                var mailAttachment = new Attachment(
                    stream,
                    attachment.FileName,
                    attachment.ContentType);

                mailMessage.Attachments.Add(mailAttachment);
            }
        }
        await smtpClient.SendMailAsync(mailMessage);
    }
}