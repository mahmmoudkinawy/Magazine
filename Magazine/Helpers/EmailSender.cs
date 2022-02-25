namespace Magazine.Helpers;
public class EmailSender : IEmailSender
{
    public Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        //Fake implemention just to avoid the IEmailSender error
        return Task.CompletedTask;
    }
}