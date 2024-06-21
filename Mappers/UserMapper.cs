
using AutoMapper;
using MealMasterAPI.Models;
using MealMasterAPI.Models.Dtos;

namespace MealMasterAPI.Mappers
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<User, UserInfoDto>().ReverseMap();
            CreateMap<UserProfile, ProfileUserDto>().ReverseMap();
        }
    }
}
