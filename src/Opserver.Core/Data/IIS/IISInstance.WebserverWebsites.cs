using System.Collections.Generic;
using System.Linq;

namespace Opserver.Data.IIS
{
    public partial class IISInstance
    {
        private Cache<List<IISWebServerWebsite>> _webServerWebsites;

        public Cache<List<IISWebServerWebsite>> WebServerWebsites =>
            _webServerWebsites ??= GetIISCache(nameof(WebServerWebsites), async () =>
            {
                var result = await API.Get<IISWebServerWebsitesResult>("webserver/websites");
                return result.Websites.Select(s => new IISWebServerWebsite {Name = s.Name, Id = s.Id, Status = s.Status}).ToList();
            });

        public class IISWebServerWebsite
        {
            public string Name { get; set; }
            public string Id { get; set; }
            public string Status { get; set; }
        }
    }
}
