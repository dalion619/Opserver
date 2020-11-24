using System.Collections.Generic;
using Opserver.Data;
using Opserver.Data.IIS;

namespace Opserver.Views.IIS
{
    public class DashboardModel
    {
        public enum LastRunInterval
        {
            FiveMinutes = 5 * 60,
            Hour = 60       * 60,
            Day = 24        * 60 * 60,
            Week = 7        * 24 * 60 * 60
        }

        public IISInstance CurrentInstance { get; set; }
        public string ErrorMessage { get; set; }

        public int Refresh { get; set; }

        public List<IISInstance.IISWebServerInfo> Connections { get; set; }
        public Cache Cache { get; set; }
    }
}
