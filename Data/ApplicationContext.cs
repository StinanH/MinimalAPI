//Stina Hedman
//NET23

using Microsoft.EntityFrameworkCore;
using MinimalAPI.Models;

namespace MinimalAPI.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Webpage> Webpages { get; set; }
        public DbSet<Interest> Interests { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options): base (options) {  }
    }
}
