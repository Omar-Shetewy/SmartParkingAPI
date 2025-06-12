namespace SmartParking.API.Data.Models
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public string Token { get; set; } = string.Empty;
        public DateTime ExpireOn { get; set; }
        public bool IsExpired => DateTimeOffset.UtcNow > ExpireOn;
        public DateTime CreatedOn { get; set; }

        //public DateTime? RevokedOn { get; set; }

        //public bool IsActive => RevokedOn == null && !IsExpired;
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
