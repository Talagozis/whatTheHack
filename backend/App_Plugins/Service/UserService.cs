using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using whatTheHack.App_Plugins.Dao;
using whatTheHack.App_Plugins.Service;
using whatTheHack2.Models.Database;

namespace whatTheHack2.App_Plugins.Service
{
    public class UserService : Service<AspNetUsers, UserDao>
    {
        public AspNetUsers findById(string userId)
        {
            return base.tDao.select().Where(a => a.Id == userId).Single();
        }

    }



}
