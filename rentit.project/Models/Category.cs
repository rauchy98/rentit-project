using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RentIt.Project.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Icon { get; set; }
        public virtual List<Product> Products { get; set; }
        public virtual List<Category> Subcategories { get; set; }
        public virtual Category Categoty { get; set; }
        public virtual List<Filter> Filters { get; set; }
    }
    public class CategoryDTO
    {
        
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Icon { get; set; }
        public List<CategoryDTO> Subcategories { get; set; }
    }
}