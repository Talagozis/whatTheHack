using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using whatTheHack.App_Plugins.Dao;
using whatTheHack2.Models.Database;

namespace whatTheHack.App_Plugins.Dao
{
    public class UserDao : IDao<AspNetUsers>
    {
        private whatTheHackEntities w;

        public UserDao()
        {
            w = new whatTheHackEntities();
        }

        public UserDao(whatTheHackEntities w)
        {
            this.w = w;
        }


        public List<AspNetUsers> select()
        {
            return w.AspNetUsers.ToList();
        }

        public AspNetUsers select(int id)
        {
            return w.AspNetUsers.Where(a => a.id == id).Single();
        }



        public AspNetUsers create(AspNetUsers tModel)
        {
            throw new NotImplementedException();
        }

        public Task<AspNetUsers> createAsync(AspNetUsers tModel)
        {
            throw new NotImplementedException();
        }

        public bool delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> deleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public AspNetUsers update(AspNetUsers tModel)
        {
            throw new NotImplementedException();
        }

        public Task<AspNetUsers> updateAsync(AspNetUsers tModel)
        {
            throw new NotImplementedException();
        }


        public async Task<List<AspNetUsers>> selectAsync()
        {
            return await Task.Run(() => this.select());
        }

        public async Task<AspNetUsers> selectAsync(int id)
        {
            return await Task.Run(() => this.select(id));
        }

    }



}
