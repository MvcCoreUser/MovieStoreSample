using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieStore.BusinessLogic.Interfaces;
using MovieStore.DataAccess.Repositories;

namespace MovieStore.BusinessLogic.Services
{
    public class ServiceCreator : IServiceCreator
    {
        public IMovieService CreateMovieService(string connectionName)
        => new MovieService(new IdentityContext(connectionName));

        public IUserService CreateUserService(string connectionName)
        => new UserService(new IdentityContext(connectionName));
    }
}
