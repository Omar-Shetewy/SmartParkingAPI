﻿namespace SmartParking.API.Data.DTO;

public class AuthResponseDTO
{
    public int UserId { get; set; }
    public string? Token { get; set; }
    public string? RefreshToken { get; set; }
    public bool IsVerified { get; set; }
}
