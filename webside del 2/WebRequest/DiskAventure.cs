using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebRequest
{
    internal class DiskAventure : IGetResponse
    {
        public string GetResponse(string url)
        {
            return GetFile(url).Result;
        }

        private async Task<string> GetFile(string path)
        {
            if (File.Exists(path))
            {
                return await File.ReadAllTextAsync(path);
            }
            return "sut";
        }
    }
}
