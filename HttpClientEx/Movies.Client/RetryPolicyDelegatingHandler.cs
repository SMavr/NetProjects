using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Movies.Client
{
    public class RetryPolicyDelegatingHandler : DelegatingHandler
    {
        private readonly int maximumAmountOfRetries = 3;

        public RetryPolicyDelegatingHandler(int maximumAmountOfRetries)
            :base()
        {
            this.maximumAmountOfRetries = maximumAmountOfRetries;
        }

        public RetryPolicyDelegatingHandler(HttpMessageHandler innerHandler,
            int maximumAmountOfRetries)
        : base(innerHandler)
        {
            this.maximumAmountOfRetries = maximumAmountOfRetries;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            HttpResponseMessage respone = null;
            for (int i = 0; i < maximumAmountOfRetries; i++)
            {
                respone = await base.SendAsync(request, cancellationToken);

                if (respone.IsSuccessStatusCode)
                {
                    return respone;
                }
            }
            return respone;
        }
    }
}
