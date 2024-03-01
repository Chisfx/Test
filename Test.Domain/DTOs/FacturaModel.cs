using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Domain.DTOs
{
    public class FacturaModel
    {
        [JsonProperty(Required = Required.Always)]
        public int Id { get; set; }

        [JsonProperty(Required = Required.Always)]
        [DataType(DataType.DateTime)]
        public DateTime Fecha { get; set; }

        [JsonProperty(Required = Required.Always)]
        [DataType(DataType.Currency)]
        [Range(1, int.MaxValue, ErrorMessage = "Age range not accepted")]
        public string Monto { get; set; }

        [JsonProperty(Required = Required.Always)]
        [Range(1, int.MaxValue, ErrorMessage = "Age range not accepted")]
        public int PersonaId { get; set; }

        public PersonaModel Persona { get; set; }
        public string PersonaFullName => Persona.ToString();
    }
}
