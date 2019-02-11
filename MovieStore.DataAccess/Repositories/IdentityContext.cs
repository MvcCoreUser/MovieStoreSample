using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using MovieStore.DataAccess.EF;
using MovieStore.DataAccess.Entities;
using MovieStore.DataAccess.Identity;
using MovieStore.DataAccess.Interfaces;

namespace MovieStore.DataAccess.Repositories
{
    public class IdentityContext : IContext
    {
        private ApplicationContext db;
        private ApplicationUserManager userManager;
        private ApplicationRoleManager roleManager;
        private IUserProfileManager userProfileManager;
        private IMovieManager movieManager;
        private bool disposed = false;

        public IdentityContext(string connectionName)
        {
            db = new ApplicationContext(connectionName);
            userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));
            roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(db));
            userProfileManager = new UserProfileManager(db);
            movieManager = new MovieManager(db);
        }
        public ApplicationUserManager UserManager => userManager;

        public IUserProfileManager UserProfileManager => userProfileManager;

        public IMovieManager MovieManager => movieManager;

        public ApplicationRoleManager ApplicationRoleManager => roleManager;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    userManager.Dispose();
                    roleManager.Dispose();
                    userProfileManager.Dispose();
                    movieManager.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }
    }
}
