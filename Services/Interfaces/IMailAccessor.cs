namespace Services.Interfaces;

public interface IMailAccessor
{
    Task SendMail(string to, string subject, string displayName, MailButton mailButton, string body = null);
}

public class MailButton
{
    public string Text { get; set; } = null;
    public string Link { get; set; } = null;
}