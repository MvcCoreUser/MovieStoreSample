using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.BusinessLogic.Interfaces
{
    public interface IServiceCreator
    {
        IUserService CreateUserService(string connectionName);
        IMovieService CreateMovieService(string connectionName);
    }
}
