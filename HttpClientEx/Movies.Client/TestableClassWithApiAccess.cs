using Movies.Client.Models;
using Movies.Client.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Movies.Client
{
    public class TestableClassWithApiAccess
    {
        private readonly HttpClient httpClient;

        public TestableClassWithApiAccess(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<Movie> GetMovie(CancellationToken cancellationToken)
        {
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
                        return null;
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        // trigger a loggin flow
                        throw new UnauthorizedApiAccessException();
                    }
                }


                var stream = await response.Content.ReadAsStreamAsync();
                response.EnsureSuccessStatusCode();

                var movie = stream.ReadAndDeserializeFromJson<Movie>();
                return movie;
            }
        }
    }
}
