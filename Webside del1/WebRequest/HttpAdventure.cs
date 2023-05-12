using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebRequest
{
    internal class HttpAdventure : IGetResponse
    {
        private HttpClient HttpClient = new();

        public string GetResponse(string url)
        {
            HttpClient.BaseAddress = new Uri(url);
            return ContactUrl().Result;
        }

        private async Task<string> ContactUrl()
        {
            using HttpResponseMessage response = await HttpClient.GetAsync("w_s/");
            return await response.Content.ReadAsStringAsync();
        }
    }
}
