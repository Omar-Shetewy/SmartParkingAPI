namespace SmartParking.API.Data.DTO
{
    public class TokenDTO
    {
        public int UserId { get; set; }
        public string Token { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
    }
}
