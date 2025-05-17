namespace SmartParking.API.Data.DTO;

public class ReservationRecordTimeDTO
{
    public DateTime StartDate { get; set; } = DateTime.Now;
    public DateTime EndDate { get; set; }
}
