using System.Data.Entity;

namespace MovieRental.Models
{
    public class MovieDBContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Genre> Genres { get; set; }

        public MovieDBContext()
            : base("Anna")
        {
            //Database.SetInitializer<MovieDBContext>(new DropCreateDatabaseAlways<MovieDBContext>());
            Database.SetInitializer<MovieDBContext>(null);
        }
    }
}