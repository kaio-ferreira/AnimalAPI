using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AnimalAPI.Models
{
    public class Address
    {
        public int Id { get; set; }

        public string? Street { get; set; }

        public string? District { get; set; }

        public string? State { get; set; }

        public string? ZipCode { get; set; }

        public string? Country { get; set; }

        [ForeignKey("OwnerId")]
        public int OwnerId { get; set; }

        [JsonIgnore]
        public Owner Owner { get; set; } = null!;
    }
}