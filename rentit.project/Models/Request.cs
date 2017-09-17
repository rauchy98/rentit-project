namespace RentIt.Project.Models
{
    public class Request
    {
        public int Id { get; set; }

        public virtual User User { get; set; }

        public virtual Product Product { get; set; }
    }
}