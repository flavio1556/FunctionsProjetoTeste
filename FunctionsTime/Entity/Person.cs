using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FunctionsAPP.Entity
{
    [Table("person_blob")]
    public class Person : BaseEntity
    {
        [Column("cpf")]
        [JsonPropertyName("cpf")]
        public long CPF { get; set; }

        [Column("name")]
        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [Column("full_Name")]
        [JsonPropertyName("full_Name")]
        public string FullName { get; set; }

        [Column("blob_Name")]
        public string BlobName { get; set; }
    }
}
