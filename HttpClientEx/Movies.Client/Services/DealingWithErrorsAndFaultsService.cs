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
    public class DealingWithErrorsAndFaultsService : IIntegrationService
    {
        private readonly IHttpClientFactory httpClientFactory;
        private CancellationTokenSource cancellationTokenSource =
            new CancellationTokenSource();


        public DealingWithErrorsAndFaultsService(
            IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task Run()
        {
            await GetMovieAndDealWithInvalidRespones(cancellationTokenSource.Token);
        }

        private async Task GetMovieAndDealWithInvalidRespones(
            CancellationToken cancellationToken)
        {
            var httpClient = httpClientFactory.CreateClient("MoviesClient");
            var request = new HttpRequestMessage(
                HttpMethod.Get,
               "api/movies/hello");
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
