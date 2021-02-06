using Moq;
using Moq.Protected;
using Movies.Client;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Movies.Tests
{
    public class TestableClassUnitTests
    {
        [Fact]
        public void GetMoview_Throw401Response()
        {
            var httpClient = new HttpClient(new Return401UnauthorizedResponseHandler());
            var testableClass = new TestableClassWithApiAccess(httpClient);

            var cancellationTokenSource = new CancellationTokenSource();

            Assert.ThrowsAsync<UnauthorizedApiAccessException>(
               () => testableClass.GetMovie(cancellationTokenSource.Token));
        }


        [Fact]
        public void Test_With_With_Moq()
        {
            var unauthorizedResponseHttpMessageHandlerMock = new Mock<HttpMessageHandler>();

            unauthorizedResponseHttpMessageHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
             ).ReturnsAsync(new HttpResponseMessage()
             {
                 StatusCode = System.Net.HttpStatusCode.Unauthorized
             });


            var httpClient = new HttpClient(unauthorizedResponseHttpMessageHandlerMock.Object);
            var testableClass = new TestableClassWithApiAccess(httpClient);

            var cancellationTokenSource = new CancellationTokenSource();

            Assert.ThrowsAsync<UnauthorizedApiAccessException>(
               () => testableClass.GetMovie(cancellationTokenSource.Token));

        }
    }
}
