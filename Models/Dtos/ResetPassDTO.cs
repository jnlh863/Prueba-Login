using System.ComponentModel.DataAnnotations;

namespace MealMasterAPI.Models.Dtos
{
    public class ResetPassDTO
    {
        public string email { get; set; }
        public string token { get; set; }

    }
}
