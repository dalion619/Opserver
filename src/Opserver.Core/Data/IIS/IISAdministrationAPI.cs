using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;
using StackExchange.Utils;

namespace Opserver.Data.IIS
{
    public class IISAdministrationAPI
    {
        private Dictionary<string, string> _requestHeaders;

        public IISAdministrationAPI(IISInstance instance) => Instance = instance;

        protected IISInstance Instance { get; }
        private string APIBaseUrl => $"https://{Instance.Name}:{Instance.Port}/api/";
        private string APIToken => Instance.AccessToken;

        private IDictionary<string, string> RequestHeaders => _requestHeaders ??= new Dictionary<string, string>
                                                                                  {
                                                                                      ["access-token"] = $"Bearer {APIToken}",
                                                                                      ["accept"] = "application/hal+json"
                                                                                  };

        public async Task<T> Get<T>(string path, NameValueCollection values = null)
        {
            var result = await Http.Request(APIBaseUrl + path + (values != null ? "?" + values : ""))
                                   .AddHeaders(RequestHeaders)
                                   .ExpectJson<T>()
                                   .GetAsync();
            return result.Data;
        }

        public async Task<T> PostAsync<T>(string path, NameValueCollection values = null)
        {
            var result = await Http.Request(APIBaseUrl + path)
                                   .AddHeaders(RequestHeaders)
                                   .SendForm(values)
                                   .ExpectJson<T>()
                                   .PostAsync();
            return result.Data;
        }

        public async Task<T> DeleteAsync<T>(string path, NameValueCollection values = null)
        {
            var result = await Http.Request(APIBaseUrl + path)
                                   .AddHeaders(RequestHeaders)
                                   .SendForm(values)
                                   .ExpectJson<T>()
                                   .DeleteAsync();
            return result.Data;
        }

        public override string ToString() => string.Concat("IIS Administration API: ", Instance.Name);
    }
}
