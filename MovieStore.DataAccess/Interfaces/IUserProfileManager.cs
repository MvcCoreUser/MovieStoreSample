using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieStore.DataAccess.Entities;

namespace MovieStore.DataAccess.Interfaces
{
    public interface IUserProfileManager: IManager
    {
        void Create(UserProfile userProfile);
        void Update(UserProfile userProfile);
        UserProfile GetUserProfileById(string id);
    }
}
