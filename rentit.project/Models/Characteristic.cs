using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RentIt.Project.Models
{
    public class Characteristic
    {
        public int Id { get; set; }

        public string Value { get; set; }

        [Required]
        public virtual Filter Filter { get; set; }

        [Required]
        public virtual Product Product { get; set; }
    }

    public class CharacteristicDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }
    }
}