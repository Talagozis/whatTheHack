using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using whatTheHack2.App_Plugins.Service;
using whatTheHack2.Models.Api;
using whatTheHack2.Models.Database;

namespace whatTheHack2.Controllers.ApiControllers
{
    public class ApiController : Controller
    {
        private CultureInfo provider = CultureInfo.InvariantCulture;

        public ActionResult Track(BrowserApi browserApi)
        {
            PatientProfilService patientProfilService = new PatientProfilService();            

            Snoring snoring = new Snoring();
            snoring.startDate = DateTime.ParseExact(browserApi.timeDomainArray[0][2].ToString(), "yyyyMMddHHmmss", provider);
            snoring.endDate = DateTime.ParseExact(browserApi.timeDomainArray[browserApi.timeDomainArray.Length - 1][2].ToString(), "yyyyMMddHHmmss", provider);
            snoring.patientProfilId = patientProfilService.findByUserId(browserApi.userId).id;
            snoring.data = JsonConvert.SerializeObject(browserApi.timeDomainArray);

            SnoringService snoringService = new SnoringService();
            snoringService.create(snoring);

            return Json("OK from the utter side " + browserApi.timeDomainArray[0]);
        }


        public ActionResult getPatients(string doctorId)
        {
            PatientProfilService patientProfilService = new PatientProfilService();
            var patientProfilIds = patientProfilService.findByDoctorUserId(doctorId).Select(a => a.userId).ToArray();

            UserService userService = new UserService();
            List<PatientApi> patientApi = userService.find().Where(a => patientProfilIds.Contains(a.Id)).Select(a => new PatientApi
            {
                id = a.Id,
                forename = a.forename,
                surname = a.surname
            }).ToList();

            return Json(JsonConvert.SerializeObject(patientApi), JsonRequestBehavior.AllowGet);
        }

        public ActionResult getPatientProfil(string patientId)
        {
            PatientProfilService patientProfilService = new PatientProfilService();
            PatientProfil patientProfilIds = patientProfilService.findByUserId(patientId);

            PatientProfilApi patientProfilApi = new PatientProfilApi();
            patientProfilApi.id = patientProfilIds.id;
            patientProfilApi.userId = patientProfilIds.userId;
            patientProfilApi.heartDisease = patientProfilIds.heartDisease;
            patientProfilApi.asthma = patientProfilIds.asthma;
            patientProfilApi.chronicCough = patientProfilIds.chronicCough;
            patientProfilApi.obesity = patientProfilIds.obesity;
            patientProfilApi.snoringApis = patientProfilIds.Snoring.Select(a => new SnoringApi
            {
                id = a.id,
                startDate = a.startDate,
                endDate = a.endDate,
            }).ToArray();

            return Json(JsonConvert.SerializeObject(patientProfilApi), JsonRequestBehavior.AllowGet);
        }

        public ActionResult getSnoring(int snoringId)
        {
            SnoringService snoringService = new SnoringService();
            Snoring snoring = snoringService.find(snoringId);
            
            return Content(snoring.data);
        }

    }
}
