namespace SmartParkingAPI.Helpers;

public class MappingProfile: Profile
{
    public MappingProfile()
    {
        CreateMap<RegisterationDTO, User>();
        CreateMap<Car, CarDetailsDTO>();
        CreateMap<CarDetailsDTO, Car>();
        CreateMap<CarDTO, Car>();
        CreateMap<GarageDTO,Garage>();
        CreateMap<Garage,GarageDTO>();
        CreateMap<ReservationRecord,ReservationRecordDetailsDTO>();
        CreateMap<ReservationRecord,ReservationRecordDTO>();
        CreateMap<ReservationRecordDTO,ReservationRecord>();
    }
}
