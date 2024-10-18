using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AnimalAPI.Models
{
    public class Animal
    {
        [Key]
        public int Id { get; set; }

        public string? Name { get; set; }

        public int Age { get; set; }

        public string? Race { get; set; }

        [ForeignKey("OwnerId")]
        public int OwnerId { get; set; }

        [JsonIgnore]
        public Owner Owner { get; set; } = null!;

    }
}