using System;
using System.ComponentModel.DataAnnotations;

namespace RentIt.Project.Models
{
    public class Order
    {
        public int Id { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime Start { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime End { get; set; }
        public int Amount { get; set; }
        public double Price { get; set; }
        public OrderStatus Status { get; set; }
        public virtual User User { get; set; }
        public virtual Product Product { get; set; }
    }
    public class OrderDTO
    {
        public int Id { get; set; }
        [Required]
        public DateTime Start { get; set; }
        [Required]
        public DateTime End { get; set; }
        [Required]
        public OrderStatus Status { get; set; }
        public int Amount { get; set; }
        public double Price { get; set; }
        public int ProductId { get; set; }
    }

    public enum OrderStatus
    {
        UnConfirmed, Confirmed, Rented
    }
}