using MealMasterAPI.Models;
using MealMasterAPI.Models.Dtos;

namespace MealMasterAPI.Repository.IRepository
{
    public interface IUser
    {
        ICollection<User> GetUsers();

        User GetUser(int userid);

        User CreateUser(RegisterDTO user);

        string UpdateUser(int id, UserDTO userdto);

        string DeleteUser(int userid);

        UserTokenDTO LoginUser(LoginDTO userDTO);

        string UpdateRol(int id, string role);

        bool Guardar();
    }
}
