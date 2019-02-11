using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MovieStore.BusinessLogic.Interfaces;

namespace MovieStore.Web
{
    public class ServiceInvoker
    {
        public IUserService UserService { get;}
        public IMovieService MovieService { get;  }
        public ServiceInvoker(Func<IUserService> userServiceCallback, Func<IMovieService> movieServiceCallback)
        {
            UserService = userServiceCallback.Invoke();
            MovieService = movieServiceCallback.Invoke();
        }
    }

    public class ServiceFactory
    {
        public static ServiceInvoker ServiceInvoker{ get; set; }
        public static IUserService UserService
            => ServiceInvoker.UserService;
        public static IMovieService MovieService
            => ServiceInvoker.MovieService;
    }

}