using System.ComponentModel.DataAnnotations;

namespace MealMasterAPI.Models.Dtos
{
    public class ProfileDTO
    {
        public string sex { get; set; } = null!;

        public int stature { get; set; }

        public int weight { get; set; }

        public string protocol { get; set; } = null!;

        public DateTime birthdate { get; set; }

    }
}
