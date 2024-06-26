using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MealMasterAPI.Models
{  
    [PrimaryKey(nameof(id))]
    public class UserProfile
    {
        [ForeignKey("User")]
        public Guid id { get; set; }

        [Required(ErrorMessage = "This field is obligatory")]
        [MaxLength(12)]
        public string sex { get; set; } = null!;

        [Required(ErrorMessage = "This field is obligatory")]
        public int stature { get; set; }

        [Required(ErrorMessage = "This field is obligatory")]
        public int weight { get; set; }

        [Required(ErrorMessage = "This field is obligatory")]
        public string protocolo { get; set; } = null!;

        public User? user { get; set; }


    }
}
