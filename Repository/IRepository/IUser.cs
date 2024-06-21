using MealMasterAPI.Models;
using MealMasterAPI.Models.Dtos;

namespace MealMasterAPI.Repository.IRepository
{
    public interface IUser
    {
        User GetUser(Guid userid);

        string CreateUser(RegisterDTO user);

        string UpdateUser(Guid id, UserDTO userdto);

        string DeleteUser(Guid userid);

        UserTokenDTO RequestPasswordResetToken(ForgotPassDTO request);

        UserTokenDTO LoginUser(LoginDTO userDTO);

        bool ResetPassword(string email, string token, string newpassword);

        void SendPasswordResetEmail(string email, string resetLink);

        bool Guardar();
    }
}
