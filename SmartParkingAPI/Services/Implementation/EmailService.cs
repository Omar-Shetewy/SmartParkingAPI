
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace SmartParking.API.Services.Implementation
{

    public class EmailService : IEmailServices
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _config;



        public EmailService(ApplicationDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public async Task SendVerificationCodeAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            var code = new Random().Next(100000, 999999).ToString();

            var verificationCode = new UserVerificationCode
            {
                Code = code,
                UserId = userId,
                ExpirationDate = DateTime.UtcNow.AddMinutes(5),
                IsUsed = false
            };

            _context.UserVerificationCodes.Add(verificationCode);
            await _context.SaveChangesAsync();

            var body = $@"<p>Hi there!</p><p>Your verification code is: <b>{code}</b></p><p>Please enter this code in the app to confirm your email.</p> <p>Thanks,</p> <p>The iSpot Team</p>";
            await SendEmailAsync(user.Email, "Verify your email for iSpot", body);
        }

        public async Task<bool> VerifyCodeAsync(int userId, string code)
        {
            var verificationCode = await _context.UserVerificationCodes
                .Where(vc => vc.UserId == userId && vc.Code == code && !vc.IsUsed)
                .OrderByDescending(vc => vc.ExpirationDate)
                .FirstOrDefaultAsync();
            if (verificationCode == null || verificationCode.ExpirationDate < DateTime.UtcNow)
                return false;

            verificationCode.IsUsed = true;

            var user = await _context.Users.FindAsync(userId);
            user.IsVerified = true;

            await _context.SaveChangesAsync();
            return true;
        }


        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress(_config["EmailSettings:DisplayName"], _config["EmailSettings:From"]));
            email.ReplyTo.Add(new MailboxAddress(_config["EmailSettings:DisplayName"], _config["EmailSettings:From"]));
            email.To.Add(MailboxAddress.Parse(toEmail));
            email.Subject = subject;
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = body
            };

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_config["EmailSettings:SmtpServer"], int.Parse(_config["EmailSettings:Port"]), SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_config["EmailSettings:Username"], _config["EmailSettings:Password"]);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }
}
