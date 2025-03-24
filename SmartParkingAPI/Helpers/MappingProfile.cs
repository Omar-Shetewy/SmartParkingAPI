﻿namespace SmartParkingAPI.Helpers;

public class MappingProfile: Profile
{
    public MappingProfile()
    {
        CreateMap<RegisterationDTO, User>();
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
