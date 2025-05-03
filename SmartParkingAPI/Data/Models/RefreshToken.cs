﻿namespace SmartParking.API.Data.Models
{
    [Owned]
    public class RefreshToken
    {
        public string Token { get; set; }
        public DateTime ExpireOn { get; set; }
        public bool IsExpired => DateTime.UtcNow > ExpireOn;
        public DateTime CreatedOn { get; set; }
        public DateTime? RevokedOn { get; set; }
        public bool IsActive => RevokedOn == null && !IsExpired;

    }
}
