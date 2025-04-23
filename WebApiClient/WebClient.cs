using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

namespace WebApiClient
{
    public class WebClient<T> : IWebClient<T>
    {
        UriBuilder uriBuilder;
        HttpRequestMessage request;
        HttpResponseMessage response;

        public WebClient()
        {
            this.uriBuilder = new UriBuilder();
            this.request = new HttpRequestMessage();
        }

        public string Schema
        {
            set { this.uriBuilder.Scheme = value; }
        }
        public string Host
        {
            set { this.uriBuilder.Host = value; }
        }
        public int Port
        {
            set { this.uriBuilder.Port = value; }
        }
        public string Path
        {
            set { this.uriBuilder.Path = value; }
        }

        public void AddParam(string name, string value)
        {
            if (this.uriBuilder.Query == string.Empty)
                this.uriBuilder.Query = "?";
            else
                this.uriBuilder.Query += "&";

            this.uriBuilder.Query += $"{name}={value}";
        }

        public async Task<T> Get()
        {
            this.request.Method = HttpMethod.Get;
            this.request.RequestUri = new Uri(this.uriBuilder.ToString());

            using (HttpClient client = new HttpClient())
            {
                this.response = await client.SendAsync(this.request);

                if (this.response.IsSuccessStatusCode)
                {
                    string content = await this.response.Content.ReadAsStringAsync();
                    Console.WriteLine("Response: " + content); // לבדוק מה מחזיר השרת

                    if (typeof(T) == typeof(string))
                    {
                        return (T)(object)content; // החזר כמחרוזת אם T הוא string
                    }
                    else
                    {
                        return JsonConvert.DeserializeObject<T>(content); // JSON המרה ל-
                    }
                }
            }

            return default(T);
        }

        public void SetUrl(string url)
        {
            this.request.RequestUri = new Uri (url);
        }

        public Task<bool> Post(T model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Post(T model, Stream file)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Post(T model, List<Stream> file)
        {
            throw new NotImplementedException();
        }
    }
}
