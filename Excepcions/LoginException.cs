namespace MealMasterAPI.Excepcions
{
    public class LoginException : Exception
    {
        public LoginException() : base("User not found or invalidad password.")
        {
        }
    }
}
