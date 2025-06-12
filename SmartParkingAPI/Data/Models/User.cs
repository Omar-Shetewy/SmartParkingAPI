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
    public DateTime CreatedOn { get; set; } = DateTime.Now;
    public int RoleId { get; set; }
    public Role Role { get; set; }
    public string? RefreshToken { get; set; } = string.Empty;
    public DateTime? RefreshTokenExpiryTime { get; set; }
    public List<Car> Cars { get; set; }
    public ReservationRecord Reservation { get; set; }
    public UserVerificationCode? VerificationCode { get; set; } 
    public ICollection<RefreshToken>? RefreshTokens { get; set; }
}
