using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;

namespace whatTheHack2.WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Snoring" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Snoring.svc or Snoring.svc.cs at the Solution Explorer and start debugging.
    public class Snoring : ISnoring
    {
        public string DoWork()
        {

            return "ok";
        }

        public string UploadAudio()
        {
            throw new NotImplementedException();
        }
    }
}
