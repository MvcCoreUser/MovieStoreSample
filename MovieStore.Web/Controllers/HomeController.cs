using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using MovieStore.BusinessLogic.Infrastructure;
using MovieStore.BusinessLogic.Interfaces;
using MovieStore.BusinessLogic.ViewModels;
using MovieStore.Web.Infrastructure;
using Newtonsoft.Json;

namespace MovieStore.Web.Controllers
{
    [Authorize(Roles ="user, admin")]
    [RequireHttps]
    public class HomeController : Controller
    {
        private IUserService userService
            => HttpContext.GetOwinContext().GetUserManager<IUserService>();
        private IMovieService movieService
            => HttpContext.GetOwinContext().Get<IMovieService>();

       

        [HttpGet]
        public JsonResult GetMovies(int? page, int? limit, string sortBy, string direction, string searchString = null)
        {
            int total;
            List<MovieViewModel> records = GridHelper.GetMovies(page, limit, sortBy, direction, searchString).ToList();
            total = records.Count;
            return Json(new { records, total }, JsonRequestBehavior.AllowGet);
        }

        
        public ActionResult Index()
        {
           
            return View();
        }

       
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult AddMovie()
        {
            MovieViewModel model = new MovieViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddMovie(MovieViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.UserId = userService.GetUserId(User.Identity.Name);
                OperationResult operationResult= await movieService.CreateAsync(model);
                if (operationResult.Succedeed)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, operationResult.Message);
                    return View(model);
                }
            }
            return View(model);
        }

        public ActionResult EditMovie(int id)
        {
            if (User.IsMovieEditor(id))
            {
                MovieViewModel model = movieService.GetMovieById(id);
                return View(model);
            }
            else
            {
                return HttpNotFound();
            }
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditMovie(MovieViewModel model)
        {
            if (ModelState.IsValid)
            {
                OperationResult operationResult = await movieService.UpdateAsync(model);
                if (operationResult.Succedeed)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, operationResult.Message);
                    return View(model);
                }
            }
            return View(model);
        }

        public ActionResult MovieDetails(int id)
        {
            MovieViewModel model = movieService.GetMovieById(id);
            if (model!=null)
            {
                return View(model);
            }
            return HttpNotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteMovie(MovieViewModel model)
        {
            if (User.IsMovieEditor(model.Id))
            {
                await movieService.DeleteAsync(model.Id);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return HttpNotFound();
            }
        }
    }
}