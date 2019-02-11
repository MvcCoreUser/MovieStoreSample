using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using MovieStore.BusinessLogic.Infrastructure;
using MovieStore.BusinessLogic.Interfaces;
using MovieStore.BusinessLogic.ViewModels;


namespace MovieStore.Web.Controllers
{   
    [AllowAnonymous]
    [RequireHttps]
    public class AccountController : Controller
    {
        private IUserService UserService
            => HttpContext.GetOwinContext().GetUserManager<IUserService>();

        private IAuthenticationManager AuthenticationManager
            => HttpContext.GetOwinContext().Authentication;

        private async Task SetInitialDataAsync()
        {
            await UserService.SetInitialDataAsync(
                new UserViewModel
                {
                    Email = "perekhodko-evgen@mail.ru",
                    Password = "zaqwsx123.",
                    Name = "Переходько Евгений Анатольевич",
                    Phone = "89000000000",
                    Role = "Admin",

                },
                new List<string> { "user", "admin" }
                );
        }

        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                string userName = UserService.GetUserName(model.Email);
                UserViewModel userVM = new UserViewModel { Name=userName, Password = model.Password };
                ClaimsIdentity claim = await UserService.AuthentificateAsync(userVM);
                if (claim==null)
                {
                    ModelState.AddModelError(string.Empty, "Неверный логин или пароль");
                }
                else
                {
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent=model.RememberMe}, claim);
                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                UserViewModel userVM = new UserViewModel
                {
                    Email = model.Email,
                    Password = model.Password,
                    Phone = model.Phone,
                    Name = model.Name,
                    Role = "user"
                };
                OperationResult operationResult = await UserService.CreateAsync(userVM);
                if (operationResult.Succedeed)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, operationResult.Message);
                }
            }
            return View();
        }
    }
}