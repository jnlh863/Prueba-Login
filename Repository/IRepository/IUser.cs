using MealMasterAPI.Models;
using MealMasterAPI.Models.Dtos;

namespace MealMasterAPI.Repository.IRepository
{
    public interface IUser
    {
        User GetUser(Guid userid);

        string CreateUser(RegisterUserDto user);

        string UpdateUser(Guid id, UserInfoDto userdto);

        string DeleteUser(Guid userid);

        TokenUserDto RequestPasswordResetToken(ForgotPasswordDto request);

        TokenUserDto LoginUser(LoginUserDto userDTO);

        bool ResetPassword(string email, string token, string newpassword);

        void SendPasswordResetEmail(string email, string resetLink);

        bool Guardar();
    }
}
