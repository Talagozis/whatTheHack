using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace whatTheHack2.Models.Api
{
    public class BrowserApi
    {
        public string userId { get; set; }
        public string sessionId { get; set; }
        public string[][] timeDomainArray { get; set; }
    }

    public class TimeDomain
    {
        public object data { get; set; }
        //public double? min { get; set; }
        //public double? max { get; set; }
        //public DateTime date { get; set; }
    }


    public class PatientApi
    {
        public string id { get; set; }
        public string forename { get; set; }
        public string surname { get; set; }
    }

    public class PatientProfilApi
    {
        public int id { get; set; }
        public string userId { get; set; }
        public SnoringApi[] snoringApis { get; set; }
        public int? heartDisease { get; set; }
        public int? asthma { get; set; }
        public int? chronicCough { get; set; }
        public int? obesity { get; set; }
    }

    public class SnoringApi
    {
        public int id { get; set; }
        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }
        public string data { get; set; }
    }

}
