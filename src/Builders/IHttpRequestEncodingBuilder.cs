using System.Text;

namespace gifty.Shared.Builders
{
    public interface IHttpRequestEncodingBuilder
    {
         IHttpRequestAuthBuilder WithEncoding(Encoding encoding);
    }
}