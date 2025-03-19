using SmartParking.API.Data.DTO;

namespace SmartParkingAPI.Helpers;

public class MappingProfile: Profile
{
    public MappingProfile()
    {
        CreateMap<RegisterationDTO, User>();
        CreateMap<Car, CarDetailsDTO>();
        CreateMap<CarDetailsDTO, Car>();
        CreateMap<CarDTO, Car>();
    }
}
