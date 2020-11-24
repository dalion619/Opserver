using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Opserver.Data.IIS
{
    public partial class IISInstance : PollNode<IISModule>, ISearchableNode
    {
        private TimeSpan? _refreshInterval;

        public IISInstance(IISModule module, IISSettings.Server settings) : base(module, settings.Name)
        {
            Settings = settings;
            API = new IISAdministrationAPI(this);
        }

        public string FriendlyName => Settings.FriendlyName;
        public int Port => Settings.Port;
        public virtual string Description => Settings.Description;
        public TimeSpan RefreshInterval => _refreshInterval ??= Settings.RefreshIntervalSeconds.Seconds();
        public string AccessToken => Settings.AccessToken;
        protected IISSettings.Server Settings { get; }
        public IISAdministrationAPI API { get; }

        public override string NodeType => "IIS";
        public override int MinSecondsBetweenPolls => 2;

        public override IEnumerable<Cache> DataPollers
        {
            get
            {
                yield return WebServerInfo;
                yield return WebServerMonitoring;
                yield return WebServerAppPools;
                yield return WebServerWebsites;
            }
        }

        public string Name => Settings.Name;
        public string CategoryName => "IIS";
        string ISearchableNode.DisplayName => Name;

        protected override IEnumerable<MonitorStatus> GetMonitorStatus()
        {
            if (!HasPolled)
            {
                yield return MonitorStatus.Unknown;
            }

            if (HasPolled && !HasPolledCacheSuccessfully)
            {
                yield return MonitorStatus.Critical;
            }
        }

        protected override string GetMonitorStatusReason() => null;

        private Cache<T> GetIISCache<T>(
            string opName,
            Func<Task<T>> get,
            TimeSpan? cacheDuration = null,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0
        ) where T : class =>
            new Cache<T>(this, "IIS GET: " + Name + ": " + opName,
                cacheDuration ?? RefreshInterval,
                get,
                addExceptionData: e => e.AddLoggedData("Server", Name)
                                        .AddLoggedData("Host", "ConnectionInfo.Host")
                                        .AddLoggedData("Port", "Port.ToString()"),
                timeoutMs: 10000,
                memberName: memberName,
                sourceFilePath: sourceFilePath,
                sourceLineNumber: sourceLineNumber
            );
    }
}
