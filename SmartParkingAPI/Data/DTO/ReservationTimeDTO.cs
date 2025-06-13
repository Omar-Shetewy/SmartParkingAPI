namespace SmartParking.API.Data.DTO;

public class ReservationTimeDTO
{
    public int ReservationId { get; set; }
    public DateTime StartDate { get; set; } = DateTime.Now;
    public DateTime EndDate { get; set; }
}
