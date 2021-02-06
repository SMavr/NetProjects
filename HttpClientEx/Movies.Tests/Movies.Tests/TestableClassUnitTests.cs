using Movies.Client;
using System;
using System.Net.Http;
using System.Threading;
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
    }
}
