﻿namespace SmartParking.API.Data.DTO;

public class GarageDTO
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public int? OwnerId { get; set; } = null;   
    public double latitude { get; set; }
    public double longitude { get; set; }
}
