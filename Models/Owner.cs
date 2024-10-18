using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace AnimalAPI.Models
{
    public class Owner
    {
        [Key]
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Age { get; set; }

        public string? Occupation { get; set; }

        public ICollection<Animal> Animals { get; } = new List<Animal>();
        public ICollection<Address> Addressess { get; } = new List<Address>();

    }
}