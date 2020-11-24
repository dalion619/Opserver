using System.Collections.Generic;
using Opserver.Data.IIS;

namespace Opserver.Views.IIS
{
    public class ServersModel : DashboardModel
    {
        public List<IISInstance> StandaloneInstances { get; set; }
    }
}
