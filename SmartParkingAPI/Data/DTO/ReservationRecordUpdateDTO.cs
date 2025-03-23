namespace SmartParking.API.Data.DTO;

public class ReservationRecordUpdateDTO
{
    public DateTime StartDate { get; set; } = DateTime.Now;
    public DateTime EndDate { get; set; }
}
