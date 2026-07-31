namespace T2507E_ASP.Services.Impl;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;
    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public void SendEmail(string email, string subject, string body)
    {
        var smtpHost = _configuration.GetValue<string>("Email:SmtpHost");
        var smtpPort = int.Parse(_configuration["Email:SmtpPort"]!);
        var smtpUser = _configuration["Email:Username"];
        var smtpPassword = _configuration["Email:Password"];
        
    }
}