﻿namespace SmartParkingAPI.Helpers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<RegisterDTO, User>().ForMember(x => x.PasswordHash, opt => opt.Ignore());
        CreateMap<RegisterDTO, User>().ForMember(x => x.RoleId, opt => opt.Ignore());

        CreateMap<Car, CarDetailsDTO>();
        CreateMap<CarDetailsDTO, Car>();
        CreateMap<CarDTO, Car>();
        CreateMap<Car, CarDTO>();

        CreateMap<GarageDetailsDTO, Garage>();
        CreateMap<GarageDTO, Garage>();
        CreateMap<Garage, GarageDetailsDTO>();

        CreateMap<EntryCar, EntryCarDetailsDTO>();
        CreateMap<EntryCarDTO, EntryCar>();
        CreateMap<EntryCar, EntryCarDTO>();
        CreateMap<CarPositionDTO, EntryCar>();
        CreateMap<ExitCarDTO,EntryCar>();
        CreateMap<EntryCar, ExitCarDTO>();

        CreateMap<Spot, SpotDetailsDTO>();
        CreateMap<Spot, SpotDTO>();
        CreateMap<SpotDTO, Spot>();

        CreateMap<ReservationRecord, ReservationRecordDetailsDTO>();
        CreateMap<ReservationRecord, ReservationRecordDTO>();
        CreateMap<ReservationRecordDTO, ReservationRecord>();

        CreateMap<User, UserDTO>();
        CreateMap<UserDTO, User>();

        CreateMap<PaymentMethod, PaymentMethodDetailsDTO>();
        CreateMap<PaymentMethodDetailsDTO, PaymentMethod>();
        CreateMap<PaymentMethodDTO, PaymentMethod>();

        CreateMap<Payment, PaymentDetailsDTO>();
        CreateMap<PaymentDetailsDTO, Payment>();
        CreateMap<PaymentDTO, Payment>();

        CreateMap<Job, JobDetailsDTO>();
        CreateMap<JobDTO, Job>();

        CreateMap<Employee, EmployeeDetailsDTO>();
        CreateMap<EmployeeDetailsDTO, Employee>();
        CreateMap<EmployeeDTO, Employee>();

        CreateMap<Camera, CameraDetailsDTO>();
        CreateMap<CameraDetailsDTO, Camera>();
        CreateMap<CameraDTO, Camera>();
    }
}
