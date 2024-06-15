
using AutoMapper;
using MealMasterAPI.Models;
using MealMasterAPI.Models.Dtos;

namespace MealMasterAPI.Mappers
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<UserProfile, ProfileDTO>().ReverseMap();
        }
    }
}
