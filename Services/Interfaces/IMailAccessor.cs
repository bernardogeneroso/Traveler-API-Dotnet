namespace Services.Interfaces;

public interface IMailAccessor
{
    Task SendMail(string to, string subject, string displayName, string? body = null, MailButton? button = null);
}

public class MailButton
{
    public string Text { get; set; } = string.Empty;
    public string Link { get; set; } = string.Empty;
}