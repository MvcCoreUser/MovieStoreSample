using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieStore.DataAccess.EF;
using MovieStore.DataAccess.Entities;
using MovieStore.DataAccess.Interfaces;

namespace MovieStore.DataAccess.Repositories
{
    public class UserProfileManager : IUserProfileManager
    {
        public ApplicationContext Database { get; set; }
        public UserProfileManager(ApplicationContext db)
        {
            Database = db;
        }
        public void Create(UserProfile userProfile)
        {
            Database.UserProfiles.Add(userProfile);
            Database.SaveChanges();
        }

        public void Update(UserProfile userProfile)
        {
            UserProfile profile = Database.UserProfiles.Include(u=>u.Movies).FirstOrDefault(u=>u.Id==userProfile.Id);
            if (profile!=null)
            {
                profile.Name = userProfile.Name;
                profile.Phone = userProfile.Phone;
                Database.Entry(profile).State = EntityState.Modified;
                Database.SaveChanges();
            }
            else
            {
                throw new Exception($"Профиль пользователя с Id {userProfile.Id} не найден.");
            }
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public UserProfile GetUserProfileById(string id)
         => Database.UserProfiles.FirstOrDefault(u => u.Id == id);
    }
}
