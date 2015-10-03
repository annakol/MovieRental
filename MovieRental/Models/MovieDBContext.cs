using System.Data.Entity;

namespace MovieRental.Models
{
    public class MovieDBContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }

        public MovieDBContext()
            : base("DefaultConnection")
        {

        }
    }
}