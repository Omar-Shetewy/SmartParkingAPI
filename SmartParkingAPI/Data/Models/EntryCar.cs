﻿namespace SmartParking.API.Data.Models;

public class EntryCar
{
    public int Id { get; set; }
    public string PlateNumber { get; set; }
    public DateTime EntryTime { get; set; }
    public DateTime ExitTime { get; set; }
    public bool IsPaid { get; set; } = false;
    public bool IsActive { get; set; }
    public int GarageId { get; set; }
    public int? SpotId { get; set; }
    public Garage Garage { get; set; }
    public Spot Spot { get; set; }

}
