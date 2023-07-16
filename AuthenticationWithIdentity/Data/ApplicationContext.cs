using AuthenticationWithIdentity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationWithIdentity.Data
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
      : base(options)
        {
        }
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }
        public DbSet<Page> Pages { get; set; }
        
        public DbSet<PageAction> PageActions { get; set; }
        public DbSet<Group> groups { get; set; }
        public DbSet<GroupPageAction> groupPages { get; set; }

    }
}
