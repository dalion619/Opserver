using System.Collections.Generic;
using System.Linq;

namespace Opserver.Data.IIS
{
    public partial class IISInstance
    {
        private Cache<List<IISWebServerAppPool>> _webServerAppPools;

        public Cache<List<IISWebServerAppPool>> WebServerAppPools =>
            _webServerAppPools ??= GetIISCache(nameof(WebServerAppPools), async () =>
            {
                var result = await API.Get<IISWebServerAppPoolsResult>("webserver/application-pools");
                return result.AppPools.Select(s => new IISWebServerAppPool {Name = s.Name, Id = s.Id, Status = s.Status}).ToList();
            });

        public class IISWebServerAppPool
        {
            public string Name { get; set; }
            public string Id { get; set; }
            public string Status { get; set; }
        }
    }
}
