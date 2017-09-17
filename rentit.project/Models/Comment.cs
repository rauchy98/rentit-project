using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RentIt.Project.Models
{
    public class Comment
    {
        public int Id { get; set; }

        [Required]
        public string Text { get; set; }

        public int Rate { get; set; }

        public virtual User Author { get; set; }

        public virtual Product Product { get; set; }
    }
    
    public class CommentDTO
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public int AuthorId { get; set; }

        public int ProductId { get; set; }
    }
}