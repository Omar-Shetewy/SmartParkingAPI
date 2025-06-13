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
    public string? FcmToken { get; set; }
    public byte[]? Image { get; set; }
    public DateTime CreatedOn { get; set; } = DateTime.Now;
    public Gender? Gender { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public byte? Age => DateOfBirth == null ? null : (byte)((DateTime.Today - DateOfBirth.Value).TotalDays / 365.25);
    public int RoleId { get; set; }
    public Role Role { get; set; }
    public Car Car { get; set; }
    public Reservation Reservation { get; set; }
    public UserVerificationCode VerificationCode { get; set; }
    public RefreshToken? RefreshToken { get; set; }
}
