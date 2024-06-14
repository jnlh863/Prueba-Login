using System.ComponentModel.DataAnnotations;

namespace MealMasterAPI.Models.Dtos
{
    public class UserDTO
    {
        public Guid id { get; set; } 

        public string username { get; set; } = null!;

        public string email { get; set; } = null!;

    }
}
