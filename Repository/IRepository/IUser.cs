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

        UserTokenDTO RequestPasswordResetToken(ForgotPassDTO request);

        UserTokenDTO LoginUser(LoginDTO userDTO);

        bool ResetPassword(string email, string token, string newpassword);

        string UpdateRol(Guid id, string role);

        bool Guardar();
    }
}
