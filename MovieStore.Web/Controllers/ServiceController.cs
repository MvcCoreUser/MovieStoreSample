using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using Microsoft.AspNet.Identity.Owin;
using MovieStore.BusinessLogic.Infrastructure;
using MovieStore.BusinessLogic.Interfaces;
using MovieStore.BusinessLogic.ViewModels;

namespace MovieStore.Web.Controllers
{
    public class ServiceController : ApiController
    {
        private IUserService userService
            => HttpContext.Current.GetOwinContext().GetUserManager<IUserService>();
        //GET: api/Service
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET: api/Service/5
        public async Task<StatusCodeResult> Get(long id)
        {
            if (Request.IsLocal() && id == 12345)
            {
                OperationResult operationResult= await userService.SetInitialDataAsync(
                new UserViewModel
                {
                    Email = "admin@mail.ru",
                    Password = "zaqwsx123.",
                    Name = "admin",
                    Phone = "89000000000",
                    Role = "Admin"
                },
                new List<string> { "user", "admin" }
                );
                if (operationResult.Succedeed)
                {
                    return StatusCode(HttpStatusCode.OK);
                }
                else
                {
                    return StatusCode(HttpStatusCode.InternalServerError);
                }
                
            }
            else
            {
                return StatusCode(HttpStatusCode.NotFound);
            }
        }

        //// POST: api/Service
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT: api/Service/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/Service/5
        //public void Delete(int id)
        //{
        //}
    }
}
