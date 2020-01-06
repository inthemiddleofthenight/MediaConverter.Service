using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MediaConverter.Client
{
   
    public class MediaServiceClient
    {
        private readonly string _baseUrl;
        public WebProxy Proxy { get; set; } 
        public MediaServiceClient(string baseUrl)
        {
            _baseUrl = baseUrl ?? throw new ArgumentNullException("BaseUrl is null");
        }

        public async Task<ConvertResponse> ConvertAsync(byte[] data, string inFormat, string outFormat)
        {
            using (WebClient client = new WebClient())
            {
                client.UseDefaultCredentials = Proxy == null;
                client.Proxy = Proxy;
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                client.Encoding = Encoding.UTF8;
                string payload = JsonConvert.SerializeObject(new { data, inFormat, outFormat });
                string content = await client.UploadStringTaskAsync($"{_baseUrl.TrimEnd('/')}/api/convert", payload);

                return JsonConvert.DeserializeObject<ConvertResponse>(content);
            }
        }

        public async Task<ConvertResponse> ConvertAsync(string fileName, string outFormat)
        {
            return await ConvertAsync(File.ReadAllBytes(fileName), Path.GetExtension(fileName), outFormat);
        }
    }
}
