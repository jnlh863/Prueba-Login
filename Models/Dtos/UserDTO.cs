using Newtonsoft.Json;

namespace MealMasterAPI.Models.Dtos
{
    public class UserDto
    {
        [JsonRequired]
        public Guid id { get; set; } 

        public string username { get; set; } = null!;

        public string email { get; set; } = null!;

    }
}
