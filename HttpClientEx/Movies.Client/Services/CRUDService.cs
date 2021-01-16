using Movies.Client.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
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
        }

        public async Task Run()
        {
            await GetResource();
        }


        public async Task GetResource()
        {
            var respose = await httpClient.GetAsync("api/movies");
            
            respose.EnsureSuccessStatusCode();

            var content = await respose.Content.ReadAsStringAsync();

            var movies = JsonConvert.DeserializeObject<IEnumerable<Movie>>(content);
        }
    }
}
