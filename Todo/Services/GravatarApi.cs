using Newtonsoft.Json;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Todo.Models.Gravatar;

namespace Todo.Services
{
    public class GravatarApi
    {
        private HttpClient httpClient;

        public GravatarApi(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<string> GetProfileName(string email)
        {
            var response = await httpClient.GetAsync($"{Gravatar.GetHash(email)}.json");
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return "";
            var content = await response.Content.ReadAsStringAsync();
            var responseObj = JsonConvert.DeserializeObject<Response>(content);
            var entry = responseObj.entry.FirstOrDefault();
            if (entry == null) 
                return "";
            return entry?.name.formatted ?? (entry.displayName ?? "");
        }
    }
}
