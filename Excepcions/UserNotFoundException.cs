namespace MealMasterAPI.Excepcions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException() : base("User not found.")
        {
        }
    }
}
