using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace Opserver.Data.IIS
{
    public class IISModule : StatusModule<IISSettings>
    {
        public IISModule(IConfiguration config, PollingService poller) : base(config, poller) =>
            Instances = Settings.Servers.Select(c => new IISInstance(this, c))
                                .Where(i => i.TryAddToGlobalPollers())
                                .ToList();

        public override string Name => "IIS";
        public override bool Enabled => Instances.Count > 0;
        public List<IISInstance> Instances { get; }

        public override MonitorStatus MonitorStatus => Instances.GetWorstStatus();

        public override bool IsMember(string node) =>
            Instances.Any(i => string.Equals(i.Name, node, StringComparison.InvariantCultureIgnoreCase));
    }
}
