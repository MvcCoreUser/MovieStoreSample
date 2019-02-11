using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieStore.DataAccess.Identity;

namespace MovieStore.DataAccess.Interfaces
{
    public interface IContext: IDisposable
    {
        ApplicationUserManager UserManager { get; }
        IUserProfileManager UserProfileManager { get; }
        IMovieManager MovieManager { get; }
        ApplicationRoleManager ApplicationRoleManager { get; }
        Task SaveAsync();
    }
}
