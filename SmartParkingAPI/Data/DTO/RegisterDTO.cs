﻿using Newtonsoft.Json;
using SmartParking.API.Attributes;
using System.ComponentModel.DataAnnotations;

namespace SmartParkingAPI.Data.DTO;

public class RegisterDTO
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    [StrongPassword]
    public string Password { get; set; } = string.Empty;
}
