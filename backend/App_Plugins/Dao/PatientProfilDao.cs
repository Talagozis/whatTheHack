using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using whatTheHack.App_Plugins.Dao;
using whatTheHack2.Models.Database;

namespace whatTheHack2.App_Plugins.Dao
{
    public class PatientProfilDao : IDao<PatientProfil>
    {
        private whatTheHackEntities w;

        public PatientProfilDao()
        {
            w = new whatTheHackEntities();
        }

        public PatientProfilDao(whatTheHackEntities w)
        {
            this.w = w;
        }

        public List<PatientProfil> select()
        {
            return w.PatientProfil.ToList();
        }

        public PatientProfil select(int id)
        {
            return w.PatientProfil.Where(a => a.id == id).Single();
        }


        public PatientProfil create(PatientProfil tModel)
        {
            throw new NotImplementedException();
        }

        public Task<PatientProfil> createAsync(PatientProfil tModel)
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

       

        public Task<List<PatientProfil>> selectAsync()
        {
            throw new NotImplementedException();
        }

        public Task<PatientProfil> selectAsync(int id)
        {
            throw new NotImplementedException();
        }

        public PatientProfil update(PatientProfil tModel)
        {
            throw new NotImplementedException();
        }

        public Task<PatientProfil> updateAsync(PatientProfil tModel)
        {
            throw new NotImplementedException();
        }
    }


}
