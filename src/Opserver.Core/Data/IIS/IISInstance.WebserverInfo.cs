namespace Opserver.Data.IIS
{
    public partial class IISInstance
    {
        private Cache<IISWebServerInfo> _webServerInfo;

        public Cache<IISWebServerInfo> WebServerInfo =>
            _webServerInfo ??= GetIISCache(nameof(WebServerInfo), async () =>
            {
                var result = await API.Get<IISWebServerInfoResult>("webserver/info");
                return new IISWebServerInfo {Id = result.Id, Name = result.Name, FullVersion = result.Version};
            });

        public class IISWebServerInfo
        {
            public string Version { get; internal set; }
            public string FullVersion { get; internal set; }
            public string Name { get; internal set; }
            public string Id { get; internal set; }
        }
    }
}
