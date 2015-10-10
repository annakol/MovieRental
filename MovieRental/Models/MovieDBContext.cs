using System.Data.Entity;

namespace MovieRental.Models
{
    public class MovieDBContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<User> Users { get; set; }


        public MovieDBContext()
            : base("DefaultConnection")
        {
            Database.SetInitializer<MovieDBContext>(null);
        }

        //public System.Data.Entity.DbSet<MovieRental.Models.User> Users { get; set; }
    }
}