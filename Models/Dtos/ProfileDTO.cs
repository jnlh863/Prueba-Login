using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace MealMasterAPI.Models.Dtos
{
    public class ProfileDTO
    {
        [JsonRequired]
        public Guid id { get; set; }

        public string sex { get; set; } = null!;

        [Required(ErrorMessage = "La estatura es obligatoria.")]
        [JsonProperty(Required = Required.Always)]
        public int stature { get; set; }

        [Required(ErrorMessage = "El peso es obligatorio.")]
        [JsonProperty(Required = Required.Always)]
        public int weight { get; set; }

        public string protocol { get; set; } = null!;


    }
}
