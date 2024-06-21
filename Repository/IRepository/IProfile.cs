using MealMasterAPI.Models;
using MealMasterAPI.Models.Dtos;

namespace MealMasterAPI.Repository.IRepository
{
    public interface IProfile
    {
        ProfileUserDto GetProfile(Guid userid);

        string CreateProfile(Guid id, ProfileUserDto profile);

        string UpdateProfile(Guid id, ProfileUserDto profiledto);

        bool Guardar();
    }
}
