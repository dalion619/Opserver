using System.Collections.Generic;
using Opserver.Data.IIS;

namespace Opserver
{
    public class IISSettings : ModuleSettings
    {
        public override bool Enabled => Servers.Count > 0;
        public override string AdminRole => IISRoles.Admin;
        public override string ViewRole => IISRoles.Viewer;

        public List<Server> Servers { get; set; } = new List<Server>();

        public class Server : ISettingsCollectionItem
        {
            /// <summary>
            ///     The friendly name for this node
            /// </summary>
            public string FriendlyName { get; set; }

            /// <summary>
            ///     IIS Administration port for this node
            /// </summary>
            public int Port { get; set; } = 55539;

            /// <summary>
            ///     Description
            /// </summary>
            public string Description { get; set; }

            /// <summary>
            ///     The access token for this node
            /// </summary>
            public string AccessToken { get; set; }

            /// <summary>
            ///     How many seconds before polling this cluster for status again
            /// </summary>
            public int RefreshIntervalSeconds { get; set; } = 30;

            /// <summary>
            ///     The machine name for this node
            /// </summary>
            public string Name { get; set; }
        }
    }
}
