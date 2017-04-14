namespace gifty.Shared.Builders
{
    public interface IHttpRequestAuthBuilder
    {
         IHttpRequestBuilder WithAuthorizationHeader(string authorizationToken);
         IHttpRequestBuilder WithNoAuthorizationHeader();
    }
}