using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using MovieStore.BusinessLogic.ViewModels;

namespace MovieStore.Web
{
    public static class UserClaims
    {
        public static bool IsMovieEditor(this IPrincipal user, int movieId)
        {
            string userId = ServiceFactory.UserService.GetUserId(user.Identity.Name);
            MovieViewModel movie = ServiceFactory.MovieService.GetMovieById(movieId);
            return movie.UserId.Equals(userId) || IsAdmin(user);
        }

        public static bool IsAdmin(this IPrincipal user)
        {
            return user.IsInRole("admin");
        }
    }
}