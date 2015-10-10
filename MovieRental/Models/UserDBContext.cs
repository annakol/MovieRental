using System.Data.Entity;

namespace MovieRental.Models
{
    public class UserDBContext  : DbContext
    {
        public DbSet<User> Users { get; set; }

        public UserDBContext()
            : base("DefaultConnection")
        {

        }
    
    }
}