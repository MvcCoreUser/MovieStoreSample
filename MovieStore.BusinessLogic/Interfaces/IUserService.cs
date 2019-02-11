using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using MovieStore.BusinessLogic.Infrastructure;
using MovieStore.BusinessLogic.ViewModels;

namespace MovieStore.BusinessLogic.Interfaces
{
    public interface IUserService: IDisposable
    {
        Task<OperationResult> CreateAsync(UserViewModel userViewModel);
        Task<OperationResult> UpdateAsync(UserViewModel userViewModel);
        Task<ClaimsIdentity> AuthentificateAsync(UserViewModel userViewModel);
        Task<OperationResult> SetInitialDataAsync(UserViewModel userViewModel, List<string> roles);
        string GetUserName(string email);

        string GetUserId(string name);
    }
}
