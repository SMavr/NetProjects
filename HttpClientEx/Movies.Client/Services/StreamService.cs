using Movies.Client.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Client.Services
{
    public class StreamService : IIntegrationService
    {
        private static HttpClient httpClient = new HttpClient();

        public StreamService()
        {
            // set up HttpClient instance
            httpClient.BaseAddress = new Uri("http://localhost:57863");
            httpClient.Timeout = new TimeSpan(0, 0, 30);
            httpClient.DefaultRequestHeaders.Clear();
        }

        public async Task Run()
        {
            // await GetPsosterWithStream();
            // await GetPosterWithStreamAndCompletionMode();

            //    await TestGetPosterWithoutStream();
            //    await TestGetPosterWithStream();
            //    await TestGetPosterWithStreamAndCompletionMode();

            await PostPosterWithStream();
        }

        //"d8663e5e-7494-4f81-8739-6e0de1bea7ee"

        private async Task GetPosterWithStream()
        {
            var request = new HttpRequestMessage(
                HttpMethod.Get,
                $"api/movies/d8663e5e-7494-4f81-8739-6e0de1bea7ee/posters/{Guid.NewGuid()}");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Good practing when we are using stream that they need to be
            // desposed.
            using (var response = await httpClient.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();

                var stream = await response.Content.ReadAsStreamAsync();
                var poster = stream.ReadAndDeserializeFromJson<Poster>();
            }
        }

        private async Task GetPosterWithStreamAndCompletionMode()
        {
            var request = new HttpRequestMessage(
                HttpMethod.Get,
                $"api/movies/d8663e5e-7494-4f81-8739-6e0de1bea7ee/posters/{Guid.NewGuid()}");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Http request is complete when the response headers are read
            using (var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead))
            {
                response.EnsureSuccessStatusCode();

                var stream = await response.Content.ReadAsStreamAsync();
                var poster = stream.ReadAndDeserializeFromJson<Poster>();
            }
        }

        private async Task GetPosterWithoutStream()
        {
            var request = new HttpRequestMessage(
              HttpMethod.Get,
              $"api/movies/d8663e5e-7494-4f81-8739-6e0de1bea7ee/posters/{Guid.NewGuid()}");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var posters = JsonConvert.DeserializeObject<Poster>(content);
        }

        private async Task PostPosterWithStream()
        {
            // generate a move poster of 500KB
            var random = new Random();
            var generatedBytes = new byte[524288];
            random.NextBytes(generatedBytes);

            var posterForCreation = new PosterForCreation()
            {
                Name = "A new poster for The Big Lebowski",
                Bytes = generatedBytes
            };

            var memoryContentStream = new MemoryStream();
            memoryContentStream.SerializeToJsonAndWrite(posterForCreation);

            // Set the pointer of stream in the begining
            memoryContentStream.Seek(0, SeekOrigin.Begin);
            using (var request = new HttpRequestMessage(
                HttpMethod.Post,
                  $"api/movies/d8663e5e-7494-4f81-8739-6e0de1bea7ee/posters"))
            {
                request.Headers.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                using (var streamContent = new StreamContent(memoryContentStream))
                {
                    request.Content = streamContent;
                    request.Content.Headers.ContentType =
                        new MediaTypeHeaderValue("application/json");

                    var response = await httpClient.SendAsync(request);
                    response.EnsureSuccessStatusCode();

                    var createdContent = await response.Content.ReadAsStringAsync();
                    var createdPoster = JsonConvert.DeserializeObject<Poster>(createdContent);
                }
            }

            //using(var streamWriter = new StreamWriter(memoryContentStream,
            //    new UTF8Encoding(), 1024, true))
            //{
            //    using (var jsonTextWriter = new JsonTextWriter(streamWriter))
            //    {
            //        var jsonSerializer = new JsonSerializer();
            //        jsonSerializer.Serialize(jsonTextWriter, posterForCreation);
            //        jsonTextWriter.Flush();
            //    }
            //}
        }

        public async Task TestGetPosterWithoutStream()
        {
            // warmup
            await GetPosterWithoutStream();

            // start stopwatch
            var stopWatch = Stopwatch.StartNew();

            // run requests
            for (int i = 0; i < 200; i++)
            {
                await GetPosterWithoutStream();
            }

            // stop stopwatch
            stopWatch.Stop();

            Console.WriteLine($"Elapse millseconds without stream: " +
                $"{stopWatch.ElapsedMilliseconds}, " +
                $"averaging {stopWatch.ElapsedMilliseconds / 200} milliseconds/request");
        }

        public async Task TestGetPosterWithStream()
        {
            // warmup
            await GetPosterWithStream();

            // start stopwatch
            var stopWatch = Stopwatch.StartNew();

            // run requests
            for (int i = 0; i < 200; i++)
            {
                await GetPosterWithStream();
            }

            // stop stopwatch
            stopWatch.Stop();

            Console.WriteLine($"Elapse millseconds with stream: " +
                $"{stopWatch.ElapsedMilliseconds}, " +
                $"averaging {stopWatch.ElapsedMilliseconds / 200} milliseconds/request");
        }

        public async Task TestGetPosterWithStreamAndCompletionMode()
        {
            // warmup
            await GetPosterWithStreamAndCompletionMode();

            // start stopwatch
            var stopWatch = Stopwatch.StartNew();

            // run requests
            for (int i = 0; i < 200; i++)
            {
                await GetPosterWithStreamAndCompletionMode();
            }

            // stop stopwatch
            stopWatch.Stop();

            Console.WriteLine($"Elapse millseconds with stream and completionmode: " +
                $"{stopWatch.ElapsedMilliseconds}, " +
                $"averaging {stopWatch.ElapsedMilliseconds / 200} milliseconds/request");
        }
    }
}
