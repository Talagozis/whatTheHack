using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using whatTheHack.App_Plugins.Service;
using whatTheHack2.App_Plugins.Dao;
using whatTheHack2.Models.Database;

namespace whatTheHack2.App_Plugins.Service
{
    public class PatientProfilService : Service<PatientProfil, PatientProfilDao>
    {
        public PatientProfil findByUserId(string userId)
        {
            return base.tDao.select().Where(a => a.AspNetUsers.Id == userId).Single();
        }

        public List<PatientProfil> findByDoctorUserId(string userId)
        {
            return base.tDao.select().Where(a => a.doctorUserId == userId).ToList();
        }

    }
}
