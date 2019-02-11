using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using MovieStore.DataAccess.Entities;

namespace MovieStore.DataAccess.EF
{
    public class ApplicationContext: IdentityDbContext<ApplicationUser>
    {
        public ApplicationContext()
        {

        }
        public ApplicationContext(string connectionName): base(connectionName)
        {

        }
        public DbSet<UserProfile> UserProfiles{ get; set; }
        public DbSet<Movie> Movies{ get; set; }
    }
}
