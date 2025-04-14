namespace SmartParking.API.Data.Models
{
    public class UserVerificationCode
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public DateTime ExpirationDate { get; set; }
        public bool IsUsed { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
