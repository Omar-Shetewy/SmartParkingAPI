namespace SmartParking.API.Data.Models;

public class Owner
{
    public int OwnerId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Gender { get; set; } = string.Empty;
    public List<Garage> Garages { get; set; }
}
