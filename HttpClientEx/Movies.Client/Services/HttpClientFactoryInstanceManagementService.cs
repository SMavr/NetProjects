using Movies.Client.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Movies.Client.Services
{
    public class HttpClientFactoryInstanceManagementService : IIntegrationService
    {
        private CancellationTokenSource cancellationTokenSource =
          new CancellationTokenSource();

        private readonly IHttpClientFactory httpClientFactory;

        public HttpClientFactoryInstanceManagementService(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }


        public async Task Run()
        {
            //await TestDisposeHttpClient(cancellationTokenSource.Token);
            //await TestReuseHttpClient(cancellationTokenSource.Token);
            //await GetMoviesWithHttpClientFromFactory(cancellationTokenSource.Token);
            await GetMoviesWithNamedHttpClientFromFactory(cancellationTokenSource.Token);
        }

        private async Task TestDisposeHttpClient(CancellationToken cancellationToken)
        {
            for (int i = 0; i < 10; i++)
            {
                using (var httpClient = new HttpClient())
                {
                    var request = new HttpRequestMessage(
                         HttpMethod.Get,
                        "https://www.google.gr/?hl=el");

                    using (var response = await httpClient.SendAsync(request,
                        HttpCompletionOption.ResponseHeadersRead,
                        cancellationToken))
                    {
                        var stream = await response.Content.ReadAsStreamAsync();
                        response.EnsureSuccessStatusCode();

                        Console.WriteLine($"Request completed with statu code {response.StatusCode}");
                    }
                }
            }
        }

        private async Task TestReuseHttpClient(CancellationToken cancellationToken)
        {
            var httpClient = new HttpClient();

            for (int i = 0; i < 10; i++)
            {
                var request = new HttpRequestMessage(
                        HttpMethod.Get,
                    "https://www.google.gr/?hl=el");

                using (var response = await httpClient.SendAsync(request,
                    HttpCompletionOption.ResponseHeadersRead,
                    cancellationToken))
                {
                    var stream = await response.Content.ReadAsStreamAsync();
                    response.EnsureSuccessStatusCode();

                    Console.WriteLine($"Request completed with statu code {response.StatusCode}");
                }
            }
        }

        private async Task GetMoviesWithHttpClientFromFactory(
            CancellationToken cancellationToken)
        {
            var httpClient = httpClientFactory.CreateClient();
            //http://localhost:57863
            var request = new HttpRequestMessage(
                HttpMethod.Get,
                "http://localhost:57863/api/movies");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            using(var response = await httpClient.SendAsync(request,
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken))
            {
                var stream = await response.Content.ReadAsStreamAsync();
                response.EnsureSuccessStatusCode();
                var movies = stream.ReadAndDeserializeFromJson<List<Movie>>();
            }
        }

        private async Task GetMoviesWithNamedHttpClientFromFactory(
           CancellationToken cancellationToken)
        {
            var httpClient = httpClientFactory.CreateClient("MoviesClient");
            var request = new HttpRequestMessage(
                HttpMethod.Get,
               "api/movies");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));


            using (var response = await httpClient.SendAsync(request,
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken))
            {
                var stream = await response.Content.ReadAsStreamAsync();
                response.EnsureSuccessStatusCode();
                var movies = stream.ReadAndDeserializeFromJson<List<Movie>>();
            }
        }
    }
}
