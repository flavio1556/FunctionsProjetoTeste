using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FunctionsAPP.Entity
{
    public class BaseEntity
    {
        [Column("id")]
        [JsonPropertyName("id")]
        public int Id { get; set; }
    }
}
