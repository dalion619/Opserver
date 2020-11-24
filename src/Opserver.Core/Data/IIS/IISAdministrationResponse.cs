using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Opserver.Data.IIS
{
    public class IISWebServerInfoResult
    {
        [DataMember(Name = "name")] public string Name { get; set; }

        [DataMember(Name = "id")] public string Id { get; set; }

        [DataMember(Name = "supports_sni")] public bool SupportsSNI { get; set; }

        [DataMember(Name = "status")] public string Status { get; set; }

        [DataMember(Name = "version")] public string Version { get; set; }
    }

    public class IISWebServerWebsiteAppResult
    {
        [DataMember(Name = "name")] public string Name { get; set; }

        [DataMember(Name = "id")] public string Id { get; set; }

        [DataMember(Name = "status")] public string Status { get; set; }
    }

    public class IISWebServerWebsitesResult
    {
        [DataMember(Name = "websites")] public List<IISWebServerWebsiteAppResult> Websites { get; set; }
    }

    public class IISWebServerAppPoolsResult
    {
        [DataMember(Name = "app_pools")] public List<IISWebServerWebsiteAppResult> AppPools { get; set; }
    }

    public class IISWebServerMonitoringResult
    {
        [DataMember(Name = "id")] public string Id { get; set; }

        [DataMember(Name = "network")] public WebServerMonitoringNetworkResult Network { get; set; }

        [DataMember(Name = "requests")] public WebServerMonitoringRequestsResult Requests { get; set; }

        [DataMember(Name = "memory")] public WebServerMonitoringMemoryResult Memory { get; set; }

        [DataMember(Name = "cpu")] public WebServerMonitoringCpuResult Cpu { get; set; }

        [DataMember(Name = "disk")] public WebServerMonitoringDiskResult Disk { get; set; }

        [DataMember(Name = "cache")] public WebServerMonitoringCacheResult Cache { get; set; }

        public class WebServerMonitoringNetworkResult
        {
            [DataMember(Name = "bytes_sent_sec")] public long BytesSentSec { get; set; }

            [DataMember(Name = "bytes_recv_sec")] public long BytesRecvSec { get; set; }

            [DataMember(Name = "connection_attempts_sec")]
            public long ConnectionAttemptsSec { get; set; }

            [DataMember(Name = "total_bytes_sent")]
            public long TotalBytesSent { get; set; }

            [DataMember(Name = "total_bytes_recv")]
            public long TotalBytesRecv { get; set; }

            [DataMember(Name = "total_connection_attempts")]
            public long TotalConnectionAttempts { get; set; }

            [DataMember(Name = "current_connections")]
            public long CurrentConnections { get; set; }
        }

        public class WebServerMonitoringRequestsResult
        {
            [DataMember(Name = "active")] public int Active { get; set; }

            [DataMember(Name = "per_sec")] public int PerSec { get; set; }

            [DataMember(Name = "total")] public long Total { get; set; }
        }

        public class WebServerMonitoringMemoryResult
        {
            [DataMember(Name = "handles")] public long Handles { get; set; }

            [DataMember(Name = "private_bytes")] public long PrivateBytes { get; set; }

            [DataMember(Name = "private_working_set")]
            public long PrivateWorkingSet { get; set; }

            [DataMember(Name = "system_in_use")] public long SystemInUse { get; set; }

            [DataMember(Name = "installed")] public long Installed { get; set; }
        }

        public class WebServerMonitoringCpuResult
        {
            [DataMember(Name = "threads")] public long Threads { get; set; }

            [DataMember(Name = "processes")] public long Processes { get; set; }

            [DataMember(Name = "percent_usage")] public int PercentUsage { get; set; }

            [DataMember(Name = "system_percent_usage")]
            public int SystemPercentUsage { get; set; }
        }

        public class WebServerMonitoringDiskResult
        {
            [DataMember(Name = "io_write_operations_sec")]
            public long IOWriteOperationsSec { get; set; }

            [DataMember(Name = "io_read_operations_sec")]
            public long IOReadOperationsSec { get; set; }

            [DataMember(Name = "page_faults_sec")] public long PageFaultsSec { get; set; }
        }

        public class WebServerMonitoringCacheResult
        {
            [DataMember(Name = "file_cache_count")]
            public long FileCacheCount { get; set; }

            [DataMember(Name = "file_cache_memory_usage")]
            public long FileCacheMemoryUsage { get; set; }

            [DataMember(Name = "file_cache_hits")] public long FileCacheHits { get; set; }

            [DataMember(Name = "file_cache_misses")]
            public long FileCacheMisses { get; set; }

            [DataMember(Name = "total_files_cached")]
            public long TotalFilesCached { get; set; }

            [DataMember(Name = "output_cache_count")]
            public long OutputCacheCount { get; set; }

            [DataMember(Name = "output_cache_memory_usage")]
            public long OutputCacheMemoryUsage { get; set; }

            [DataMember(Name = "output_cache_hits")]
            public long OutputCacheHits { get; set; }

            [DataMember(Name = "output_cache_misses")]
            public long OutputCacheMisses { get; set; }

            [DataMember(Name = "uri_cache_count")] public long UriCacheCount { get; set; }

            [DataMember(Name = "uri_cache_hits")] public long UriCacheHits { get; set; }

            [DataMember(Name = "uri_cache_misses")]
            public long UriCacheMisses { get; set; }

            [DataMember(Name = "total_uris_cached")]
            public long TotalUrisCached { get; set; }
        }
    }
}
