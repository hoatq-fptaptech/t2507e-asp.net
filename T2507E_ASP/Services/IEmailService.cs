namespace T2507E_ASP.Services;

public interface IEmailService
{
    void SendEmail(string email, string subject, string body);
}