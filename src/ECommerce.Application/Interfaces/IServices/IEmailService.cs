     
using ECommerce.Application.Models;

namespace ECommerce.Application.Interfaces.IServices;

public interface IEmailService
{
    Task SendEmailAsync(
        string to,
        string subject,
        string body,
        List<EmailAttachment>? attachments = null);
}