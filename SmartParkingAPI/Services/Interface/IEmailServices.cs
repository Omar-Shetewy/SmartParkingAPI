namespace SmartParking.API.Services.Interface
{
    public interface IEmailServices
    {
        Task SendVerificationCodeAsync(int userId);
        Task<bool> VerifyCodeAsync(int userId, string code);
        Task SendEmailAsync(string toEmail, string subject, string body);
    }
}
