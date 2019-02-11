using System;
using System.ComponentModel.Design;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using MovieStore.BusinessLogic.Interfaces;
using MovieStore.BusinessLogic.Services;
using Owin;



namespace MovieStore.Web
{
    public partial class Startup
    {
        private IServiceCreator serviceCreator = new ServiceCreator();
        private readonly string connectionName = "DefaultConnection";
        private IUserService CreateUserService()
        => serviceCreator.CreateUserService(connectionName);
        public void ConfigureAuth(IAppBuilder app)
        {
            // Дополнительные сведения о настройке приложения см. на странице https://go.microsoft.com/fwlink/?LinkID=316888
            app.CreatePerOwinContext<IUserService>(CreateUserService);
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                LogoutPath=new PathString("/Account/Logout")
            });
           
        }
    }
}
