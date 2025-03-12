using AutoMapper;
using SmartParkingAPI.Data.DTO;
using SmartParkingAPI.Data.Models;

namespace SmartParkingAPI.Helpers
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterationDTO, User>();
        }
    }
}
