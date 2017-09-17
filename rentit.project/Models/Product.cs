using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RentIt.Project.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string PictureString { get; set; }

        public string[] Picture
        {
            get
            {
                return PictureString.Split(';');
            }
            set
            {
                var data = value;
                PictureString = String.Join(";", data);
            }
        }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public double Price { get; set; }

        public double RealPrice { get; set; }

        public AvailableStatus Available { get; set; }

        public virtual List<Order> Oredrs { get; set; }

        public virtual Category Category { get; set; }

        public virtual List<Comment> Comments { get; set; }

        public virtual User Seller { get; set; }

        public virtual List<Request> Requests { get; set; }

        public virtual List<Characteristic> Characteristics { get; set; }

    }
    public class ProductDTO
    {
        public int Id { get; set; }

        public string[] Picture { get; set; }
        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public string Date { get; set; }

        public int RequestCount { get; set; }

        [Required]
        public double Price { get; set; }

        public AvailableStatus Available { get; set; }

        public int SellerId { get; set; }

        public bool IsChecked { get; set; }

    }
    public enum AvailableStatus
    {
        NotAvailable, WaitForAvailable, Available 
    }
}