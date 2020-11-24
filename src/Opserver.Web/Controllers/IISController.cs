using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Opserver.Data.IIS;
using Opserver.Helpers;
using Opserver.Views.IIS;

namespace Opserver.Controllers
{
    [OnlyAllow(IISRoles.Viewer)]
    public class IISController : StatusController<IISModule>
    {
        public IISController(IISModule iisModule, IOptions<OpserverSettings> settings) : base(iisModule, settings)
        {
        }

        [DefaultRoute("iis")]
        public ActionResult Dashboard() => RedirectToAction(nameof(Servers));

        [Route("iis/servers")]
        public ActionResult Servers(string cluster, string node, string ag, bool detailOnly = false)
        {
            var vd = new ServersModel {StandaloneInstances = Module.Instances, Refresh = node.HasValue() ? 10 : 5};

            return View("Servers", vd);
        }

        [Route("iis/instance")]
        public ActionResult Instance(string node) =>
            // var i = Module.GetInstance(node);
            // var vd = new InstanceModel
            //          {
            //              View = SQLViews.Instance,
            //              Refresh = node.HasValue() ? 10 : 5,
            //              CurrentInstance = i
            //          };
            View();
    }
}
