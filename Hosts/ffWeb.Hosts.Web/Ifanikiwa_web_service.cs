using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ffWeb.Hosts.Web
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "Ifanikiwa_web_service" in both code and config file together.
    [ServiceContract]
    public interface Ifanikiwa_web_service
    {
        [OperationContract]
        void DoWork();
    }
}
