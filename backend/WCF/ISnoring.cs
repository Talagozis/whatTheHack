using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using System.Web;

namespace whatTheHack2.WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ISnoring" in both code and config file together.
    [ServiceContract]
    public interface ISnoring
    {
        [OperationContract]
        string DoWork();

        [OperationContract]
        string UploadAudio();

    }
}
