using System;
using System.Net;
using System.Net.Http;
using Polly;
using Polly.Retry;

namespace MasterService.API.Policies;

public class HttpClientPolicy
{
    public AsyncRetryPolicy<HttpResponseMessage> ImmediateHttpRetry { get;}
    public AsyncRetryPolicy<HttpResponseMessage> LinearHttpRetry {get;}
    public AsyncRetryPolicy<HttpResponseMessage> ExponentialHttpRetry {get;}

    public HttpClientPolicy()
    {
        ImmediateHttpRetry = Policy.HandleResult<HttpResponseMessage>(
                res => !res.IsSuccessStatusCode && res.StatusCode != HttpStatusCode.BadRequest)
            .RetryAsync(5);

        LinearHttpRetry = Policy.HandleResult<HttpResponseMessage>(
                res => !res.IsSuccessStatusCode && res.StatusCode != HttpStatusCode.BadRequest)
            .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(3));

        ExponentialHttpRetry = Policy.HandleResult<HttpResponseMessage>(
                res => !res.IsSuccessStatusCode && res.StatusCode != HttpStatusCode.BadRequest)
            .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));            
    }
}