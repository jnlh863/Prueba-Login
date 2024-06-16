namespace MealMasterAPI.Repository.IRepository
{
    public interface ISendEmail
    {
        void SendPasswordResetEmail(string email, string resetLink);
    }
}
