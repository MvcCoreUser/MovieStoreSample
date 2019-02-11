using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using MovieStore.BusinessLogic.Interfaces;
using MovieStore.BusinessLogic.Services;
using Owin;

[assembly: OwinStartup(typeof(MovieStore.Web.Startup))]

namespace MovieStore.Web
{
    public partial class Startup
    {


        private IMovieService CreateMovieService()
         => serviceCreator.CreateMovieService("DefaultConnection");

        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            // Дополнительные сведения о настройке приложения см. на странице https://go.microsoft.com/fwlink/?LinkID=316888
            app.CreatePerOwinContext<IMovieService>(CreateMovieService);
            ServiceFactory.ServiceInvoker = new ServiceInvoker(CreateUserService, CreateMovieService);
        }


    }
}
