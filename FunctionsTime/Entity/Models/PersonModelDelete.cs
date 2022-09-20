using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FunctionsAPP.Entity.Models
{
    public class PersonModelDelete
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("cpf")]
        public long CPF { get; set; }


        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("full_Name")]
        public string FullName { get; set; }
    }
}
