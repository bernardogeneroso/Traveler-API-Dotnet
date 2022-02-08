using FluentEmail.Core;
using Services.Interfaces;

namespace Providers.Mail;

public class MailAccessor : IMailAccessor
{
    private readonly IFluentEmail _mail;
    private readonly IOriginAccessor _originAccessor;
    public MailAccessor(IFluentEmail mail, IOriginAccessor originAccessor)
    {
        _originAccessor = originAccessor;
        _mail = mail;
    }

    public async Task SendMail(string to, string subject, string displayName, MailButton mailButton, string body = null)
    {
        body += body ?? "Test email of RentX";

        mailButton.Link += mailButton.Link ?? $"{_originAccessor.GetOrigin()}";
        mailButton.Text += mailButton.Text ?? $"Go to RentX";

        var path = Path.Combine(Directory.GetCurrentDirectory(), "Templates", "test.cshtml");

        var newEmail = await _mail
            .To(to)
            .Subject(subject)
            .UsingTemplateFromFile(path, new { DisplayName = displayName, Body = body, MailButton = mailButton })
            .SendAsync();

        if (!newEmail.Successful) throw new Exception("Error sending email");
    }
}
