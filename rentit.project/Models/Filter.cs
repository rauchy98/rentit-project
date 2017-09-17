using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentIt.Project.Models
{
    public class Filter
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual List<Characteristic> Characteristics { get;set;}

        public virtual Category Category { get; set; }
    }

    public class FilterDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<String> Values { get; set; }
    }
}