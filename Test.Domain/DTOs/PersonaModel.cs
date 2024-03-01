using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
namespace Test.Domain.DTOs
{
    public class PersonaModel
    {
        [JsonProperty(Required = Required.Always)]
        public int Id { get; set; }

        [JsonProperty(Required = Required.Always)]
        [MinLength(3, ErrorMessage = "Must be at least 3 characters")]
        [MaxLength(50, ErrorMessage = "It must have a maximum of 50 characters.")]
        [DataType(DataType.Text)]
        public string Nombre { get; set; } = default!;

        [JsonProperty(Required = Required.Always)]
        [MinLength(3, ErrorMessage = "Must be at least 3 characters")]
        [MaxLength(50, ErrorMessage = "It must have a maximum of 50 characters.")]
        [DataType(DataType.Text)]
        public string ApellidoPaterno { get; set; } = default!;

        [JsonProperty(Required = Required.Always)]
        [MinLength(3, ErrorMessage = "Must be at least 3 characters")]
        [MaxLength(50, ErrorMessage = "It must have a maximum of 50 characters.")]
        [DataType(DataType.Text)]
        public string ApellidoMaterno { get; set; } = default!;

        [JsonProperty(Required = Required.Always)]
        [MinLength(3, ErrorMessage = "Must be at least 3 characters")]
        [MaxLength(50, ErrorMessage = "It must have a maximum of 50 characters.")]
        public string Identificacion { get; set; } = default!;

        public override string ToString()
        {
            return $"{Nombre} {ApellidoPaterno} {ApellidoMaterno}";
        }

    }
}
