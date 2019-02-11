using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using MovieStore.BusinessLogic.Infrastructure;
using MovieStore.BusinessLogic.Interfaces;
using MovieStore.BusinessLogic.ViewModels;
using MovieStore.DataAccess.Entities;
using MovieStore.DataAccess.Interfaces;

namespace MovieStore.BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private IContext Database { get; set; }

        public UserService(IContext db)
        {
            Database = db;
            //Database.UserManager.Us
        }
        public async Task<ClaimsIdentity> AuthentificateAsync(UserViewModel userViewModel)
        {
            ClaimsIdentity claim = null;
            ApplicationUser user = await Database.UserManager.FindAsync(userViewModel.Name, userViewModel.Password);
            if (user!=null)
            {
                claim = await Database.UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            }
            return claim;
        }

        public async Task<OperationResult> CreateAsync(UserViewModel userViewModel)
        {
            try
            {
                ApplicationUser user = await Database.UserManager.FindByEmailAsync(userViewModel.Email);
                if (user == null)
                {
                    user = new ApplicationUser()
                    {
                        Email = userViewModel.Email,
                        UserName = userViewModel.Name
                    };
                    IdentityResult result = await Database.UserManager.CreateAsync(user, userViewModel.Password);
                    if (result.Succeeded == false)
                    {
                        return new OperationResult(result.Succeeded, string.Join(OperationResult.ErrorSeparator, result.Errors));
                    }

                    await Database.UserManager.AddToRoleAsync(user.Id, userViewModel.Role);

                    UserProfile userProfile = new UserProfile()
                    {
                        Id = user.Id,
                        Name = userViewModel.Name,
                        Phone = userViewModel.Phone
                    };

                    Database.UserProfileManager.Create(userProfile);
                    await Database.SaveAsync();
                    return new OperationResult(true, "Регистрация успешно пройдена");
                }
                else
                {
                    return new OperationResult(false, "Пользователь с таким логином уже существует", nameof(userViewModel.Email));
                }
            }
            catch (Exception ex)
            {

                return new OperationResult(false, ex.Message, string.Empty, ex);
            }
            
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public async Task<OperationResult> SetInitialDataAsync(UserViewModel userViewModel, List<string> roles)
        {
            foreach (string roleName in roles)
            {
                var role = await Database.ApplicationRoleManager.FindByNameAsync(roleName);
                if (role == null)
                {
                    role = new ApplicationRole { Name = roleName };
                    await Database.ApplicationRoleManager.CreateAsync(role);
                }
            }
            await Database.SaveAsync();
            return await this.CreateAsync(userViewModel);
            
        }

        public Task<OperationResult> UpdateAsync(UserViewModel userViewModel)
        {
            throw new NotImplementedException();
        }

        public string GetUserName(string email)
        => this.Database.UserManager.FindByEmail(email).UserName;

        public string GetUserId(string name)
        => this.Database.UserManager.FindByName(name).Id;
    }
}
