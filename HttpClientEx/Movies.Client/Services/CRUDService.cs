using Movies.Client.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Client.Services
{
    public class CRUDService : IIntegrationService
    {
        private static HttpClient httpClient = new HttpClient();

        public CRUDService()
        {
            // set up HttpClient instance
            httpClient.BaseAddress = new Uri("http://localhost:57863");
            httpClient.Timeout = new TimeSpan(0, 0, 30);
            
            // Good practice to clear request headers

            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json")) ;
        }

        public async Task Run()
        {
            //await GetResource()
            await GetResourceThroughRequestMessage();
        }


        public async Task GetResource()
        {
            var respose = await httpClient.GetAsync("api/movies");
            
            respose.EnsureSuccessStatusCode();

            var content = await respose.Content.ReadAsStringAsync();

            var movies = new List<Movie>();

            if(respose.Content.Headers.ContentType.MediaType == "application/json")
            {
                movies = JsonConvert.DeserializeObject<List<Movie>>(content);
            }
        }

        public async Task GetResourceThroughRequestMessage()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "api/movies");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var respose = await httpClient.SendAsync(request);

            respose.EnsureSuccessStatusCode();

            var content = await respose.Content.ReadAsStringAsync();
            var movies = JsonConvert.DeserializeObject<List<Movie>>(content);
        }
    }
}
