using System.ComponentModel.DataAnnotations;

namespace MealMasterAPI.Models.Dtos
{
    public class ProfileDTO
    {
        public Guid id { get; set; }

        public string sex { get; set; } = null!;

        public int stature { get; set; }

        public int weight { get; set; }

        public string protocol { get; set; } = null!;


    }
}
