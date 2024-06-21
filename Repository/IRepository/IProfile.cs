using MealMasterAPI.Models;
using MealMasterAPI.Models.Dtos;

namespace MealMasterAPI.Repository.IRepository
{
    public interface IProfile
    {
        ProfileDto GetProfile(Guid userid);

        string CreateProfile(Guid id, ProfileDto profile);

        string UpdateProfile(Guid id, ProfileDto profiledto);

        bool Guardar();
    }
}
