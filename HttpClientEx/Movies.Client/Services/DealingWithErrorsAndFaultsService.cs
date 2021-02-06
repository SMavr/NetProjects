using Movies.Client.Models;
using Newtonsoft.Json;
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
            //await GetMovieAndDealWithInvalidRespones(cancellationTokenSource.Token);
            await PostMovieAndHadleValidationErrors(cancellationTokenSource.Token);
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
                if (!response.IsSuccessStatusCode)
                {
                    // inspect the status code
                    if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        // show this to the user
                        Console.WriteLine("The requested move cannot be found");
                        return;
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        // trigger a loggin flow
                        return;
                    }
                }


                var stream = await response.Content.ReadAsStreamAsync();
                response.EnsureSuccessStatusCode();

                var movies = stream.ReadAndDeserializeFromJson<List<Movie>>();
            }
        }

        private async Task PostMovieAndHadleValidationErrors(
            CancellationToken cancellationToken)
        {
            var httpClient = httpClientFactory.CreateClient("MoviesClient");

            var movieToCreate = new MovieForCreation()
            {
                Title = "Reservoir Dogs",
                Description = "Test",
                DirectorId = Guid.Parse("d28888e9-2ba9-473a-a40f-e38cb54f9b35"),
                ReleaseDate = new DateTimeOffset(new DateTime(1992, 9, 2)),
                Genre = "Crime, Drama"
            };

            var serializedMovieToCreate = JsonConvert.SerializeObject(movieToCreate);

            using (var request = new HttpRequestMessage(
               HttpMethod.Post,
              "api/movies"))
            {

                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                request.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
                request.Content = new StringContent(serializedMovieToCreate);
                request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");


                using (var response = await httpClient.SendAsync(request,
                            HttpCompletionOption.ResponseHeadersRead,
                            cancellationToken))
                {

                    if (!response.IsSuccessStatusCode)
                    {
                        if(response.StatusCode == System.Net.HttpStatusCode.UnprocessableEntity)
                        {
                            var errorStream = await response.Content.ReadAsStreamAsync();
                            var validationErrors = errorStream.ReadAndDeserializeFromJson<object>();
                            Console.WriteLine(validationErrors);
                            return;
                        }
                        else
                        {
                            response.EnsureSuccessStatusCode();
                        }
                    }

                    var stream = await response.Content.ReadAsStreamAsync();
                    var movie = stream.ReadAndDeserializeFromJson<Movie>();
                }
            }
        }
    }
}
