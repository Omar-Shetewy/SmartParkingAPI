namespace SmartParkingAPI.Data.Models;

public class User
{
    public int UserId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public bool IsVerified { get; set; } = false;
    public byte[]? Image { get; set; }
    public DateTime CreatedOn { get; set; } = DateTime.Now;
    public int RoleId { get; set; }
    public Role Role { get; set; }
    public Car Car { get; set; }
    public ReservationRecord Reservation { get; set; }
    public ICollection<UserVerificationCode> VerificationCodes { get; set; } = new List<UserVerificationCode>();
    public ICollection<RefreshToken> RefreshTokens { get; set; }
}
