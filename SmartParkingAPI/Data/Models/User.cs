namespace SmartParkingAPI.Data.Models;

public class User
{
    public int UserId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string? RefreshToken { get; set; } = string.Empty;
    public DateTime? RefreshTokenExpiryTime { get; set; }
    public int IsActive { get; set; } = 1;
    public DateTime CreatedOn { get; set; } = DateTime.Now;

    public List<Car> Cars { get; set; }
    public int RoleId { get; set; }
    public Role Role { get; set; }
    public ReservationRecord Reservation { get; set; }
}
