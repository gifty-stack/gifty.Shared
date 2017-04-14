using gifty.Shared.HTTP;

namespace gifty.Shared.Builders
{
    public interface IHttpRequestContentTypeBuilder
    {
         IHttpRequestEncodingBuilder WithContentType(WebRequestContentType contentType);
    }
}