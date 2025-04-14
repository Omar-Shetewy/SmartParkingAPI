namespace SmartParking.API.Data.DTO
{
    public class AuthResponseDTO
    {
        public string? Token { get; set; }
        public bool IsVerified { get; set; }
    }
}
