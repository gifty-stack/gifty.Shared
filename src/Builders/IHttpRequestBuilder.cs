using System.Threading.Tasks;

namespace gifty.Shared.Builders
{
    public interface IHttpRequestBuilder
    {
         Task<TResponse> GetAsync<TResponse>(string endpoint);
         Task<TResponse> PostAsync<TResponse>(string endpoint, object body);

    }
}