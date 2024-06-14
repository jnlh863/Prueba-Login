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
        public string protocol { get; set; } = null!;

        [Required(ErrorMessage = "This birth date is obligatory.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [RegularExpression(@"\d{2}/\d{2}/\d{4}", ErrorMessage = "The date format is not valid. Use dd/MM/yyyy")]
        public DateTime birthdate { get; set; }

        public User user { get; set; }


    }
}
