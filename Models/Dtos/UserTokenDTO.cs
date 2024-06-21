namespace MealMasterAPI.Models.Dtos
{
    public class UserTokenDto
    {
        public string Token { get; set; } = null!;
        public Guid id { get; set; }
    }
}
