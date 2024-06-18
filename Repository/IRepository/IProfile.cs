using MealMasterAPI.Models;
using MealMasterAPI.Models.Dtos;

namespace MealMasterAPI.Repository.IRepository
{
    public interface IProfile
    {
        ProfileDTO GetProfile(Guid userid);

        string CreateProfile(Guid id, ProfileDTO profile);

        string UpdateProfile(Guid id, ProfileDTO profiledto);

        bool Guardar();
    }
}
