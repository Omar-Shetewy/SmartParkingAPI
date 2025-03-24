namespace SmartParkingAPI.Helpers;

public class MappingProfile: Profile
{
    public MappingProfile()
    {
        CreateMap<RegisterDTO, User>().ForMember(x => x.PasswordHash, opt => opt.Ignore());
        CreateMap<RegisterDTO, User>().ForMember(x => x.RoleId, opt => opt.Ignore());
        CreateMap<Car, CarDetailsDTO>();
        CreateMap<CarDetailsDTO, Car>();
        CreateMap<CarDTO, Car>();
        CreateMap<GarageDetailsDTO,Garage>();
        CreateMap<GarageDTO,Garage>();
        CreateMap<Garage,GarageDetailsDTO>();
        CreateMap<Spot, SpotDetailsDTO>();
        CreateMap<Spot, SpotDTO>();
        CreateMap<SpotDTO, Spot>();
        CreateMap<ReservationRecord,ReservationRecordDetailsDTO>();
        CreateMap<ReservationRecord,ReservationRecordDTO>();
        CreateMap<ReservationRecordDTO,ReservationRecord>();
    }
}
