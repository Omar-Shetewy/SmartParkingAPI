﻿namespace SmartParkingAPI.Data.Models;

public class User
{
    public int UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public int IsActive { get; set; } = 1;
    public DateTime CreatedOn { get; set; } = DateTime.Now;

    public List<Car> Cars { get; set; }
    public ReservationRecord Reservation { get; set; }
}
