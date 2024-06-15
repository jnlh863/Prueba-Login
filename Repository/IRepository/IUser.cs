using MealMasterAPI.Models;
using MealMasterAPI.Models.Dtos;

namespace MealMasterAPI.Repository.IRepository
{
    public interface IUser
    {
        ICollection<User> GetUsers();

        User GetUser(Guid userid);

        User CreateUser(RegisterDTO user);

        string UpdateUser(Guid id, UserDTO userdto);

        string DeleteUser(Guid userid);

        UserTokenDTO RequestPasswordReset(ResetPassDTO request);

        UserTokenDTO LoginUser(LoginDTO userDTO);

        string UpdateRol(Guid id, string role);

        bool Guardar();
    }
}
