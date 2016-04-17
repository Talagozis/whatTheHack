using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using whatTheHack.App_Plugins.Dao;
using whatTheHack2.Models.Database;

namespace whatTheHack.App_Plugins.Dao
{
    public class SnoringDao : IDao<Snoring>
    {
        private whatTheHackEntities w;

        public SnoringDao()
        {
            w = new whatTheHackEntities();
        }

        public SnoringDao(whatTheHackEntities w)
        {
            this.w = w;
        }


        public List<Snoring> select()
        {
            return w.Snoring.ToList();
        }

        public Snoring select(int id)
        {
            return w.Snoring.Where(a => a.id == id).Single();
        }



        public Snoring create(Snoring tModel)
        {
            w.Snoring.Add(tModel);
            w.SaveChanges();

            return tModel;
        }

        public Snoring update(Snoring tModel)
        {
            throw new NotImplementedException();
        }


        public bool delete(int id)
        {
            throw new NotImplementedException();
        }

        

        

        public async  Task<List<Snoring>> selectAsync()
        {
            return await Task.Run(() => this.select());
        }

        public async Task<Snoring> selectAsync(int id)
        {
            return await Task.Run(() => this.select(id));
        }

        public async Task<Snoring> createAsync(Snoring tModel)
        {
            return await Task.Run(() => this.create(tModel));
        }

        public Task<Snoring> updateAsync(Snoring tModel)
        {
            throw new NotImplementedException();
        }

        public Task<bool> deleteAsync(int id)
        {
            throw new NotImplementedException();
        }

    }



}
