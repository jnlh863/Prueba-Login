using MealMasterAPI.Models;
using MealMasterAPI.Models.Dtos;

namespace MealMasterAPI.Repository.IRepository
{
    public interface IUser
    {
        User GetUser(Guid userid);

        string CreateUser(RegisterDto user);

        string UpdateUser(Guid id, UserDto userdto);

        string DeleteUser(Guid userid);

        UserTokenDto RequestPasswordResetToken(ForgotPassDto request);

        UserTokenDto LoginUser(LoginDto userDTO);

        bool ResetPassword(string email, string token, string newpassword);

        void SendPasswordResetEmail(string email, string resetLink);

        bool Guardar();
    }
}
