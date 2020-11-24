namespace Opserver.Data.IIS
{
    public partial class IISInstance
    {
        private Cache<IISWebServerMonitoring> _webServerMonitoring;

        public Cache<IISWebServerMonitoring> WebServerMonitoring =>
            _webServerMonitoring ??= GetIISCache(nameof(WebServerMonitoring), async () =>
            {
                var webServerInfo = await WebServerInfo.GetData();
                var result = await API.Get<IISWebServerMonitoringResult>($"webserver/monitoring/{webServerInfo.Id}");

                var currentDiskIOPS = result.Disk.IOReadOperationsSec       + result.Disk.IOWriteOperationsSec;
                var currentNetworkSpeedSecond = result.Network.BytesRecvSec + result.Network.BytesSentSec;

                return new IISWebServerMonitoring
                       {
                           CurrentMemoryInUse = $"{result.Memory.PrivateWorkingSet.ToSize()}",
                           CurrentMemoryAvailable =
                               $"{decimal.Round(result.Memory.Installed - result.Memory.SystemInUse, 2).ToSize()} Available",
                           CurrentCPUPercent = result.Cpu.PercentUsage           > 0 ? $"{result.Cpu.PercentUsage}%" : "",
                           CurrentRequestsSecond = result.Requests.PerSec        > 0 ? $"{result.Requests.PerSec}/s" : "",
                           CurrentDiskIOPS = currentDiskIOPS                     > 0 ? $"{currentDiskIOPS} IOPS" : "",
                           CurrentNetworkSpeedSecond = currentNetworkSpeedSecond > 0 ? $"{currentNetworkSpeedSecond.ToSize()}/s" : "",
                           MemoryUtilization = result.Memory.PrivateWorkingSet,
                           InstalledMemory = result.Memory.Installed,
                           ExternalMemoryUtilization = result.Memory.SystemInUse,
                           ProcessUtilization = result.Cpu.PercentUsage,
                           ExternalProcessUtilization = result.Cpu.SystemPercentUsage
                       };
            });

        public class IISWebServerMonitoring
        {
            public string CurrentMemoryInUse { get; set; }
            public string CurrentMemoryAvailable { get; set; }
            public string CurrentCPUPercent { get; set; }
            public string CurrentRequestsSecond { get; set; }
            public string CurrentDiskIOPS { get; set; }
            public string CurrentNetworkSpeedSecond { get; set; }
            public int ProcessUtilization { get; internal set; }
            public long MemoryUtilization { get; internal set; }
            public long InstalledMemory { get; internal set; }
            public long ExternalMemoryUtilization { get; internal set; }
            public int ExternalProcessUtilization { get; internal set; }
        }
    }
}
