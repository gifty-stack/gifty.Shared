using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using gifty.Shared.HTTP;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace gifty.Shared.Builders
{
    public class HttpRequestBuilder : IHttpRequestBuilder, IHttpRequestAuthBuilder, IHttpRequestContentTypeBuilder, IHttpRequestEncodingBuilder
    {
        private readonly HttpClient _httpClient;

        private readonly IReadOnlyDictionary<WebRequestContentType, string> _contentTypes = new Dictionary<WebRequestContentType, string>
        {
            { WebRequestContentType.JSON, "application/json" },
            { WebRequestContentType.X_WWW_FORM_URLENCODED, "application/x-www-form-urlencoded" }
        };

        private string _contentType;
        private Encoding _encoding;

        HttpRequestBuilder(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public static IHttpRequestContentTypeBuilder Create(string host)
        {
            var baseAddress = new Uri(host);
            var httpClientHandler = new HttpClientHandler { AllowAutoRedirect = false };
            var httpClient = new HttpClient(httpClientHandler)
            {
                BaseAddress = baseAddress
            };
            return new HttpRequestBuilder(httpClient);
        }

        IHttpRequestEncodingBuilder IHttpRequestContentTypeBuilder.WithContentType(WebRequestContentType contentType)
        {
            string contentTypeName;

            var isSucceed = _contentTypes.TryGetValue(contentType, out contentTypeName);

            if (!isSucceed)
                throw new Exception();

            _contentType = contentTypeName;

            return this;
        }

        IHttpRequestAuthBuilder IHttpRequestEncodingBuilder.WithEncoding(Encoding encoding)
        {
            _encoding = encoding;
            return this;
        }

        IHttpRequestBuilder IHttpRequestAuthBuilder.WithAuthorizationHeader(string authorizationToken)
        {
            _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("authorization", $"Bearer {authorizationToken}");
            return this;
        }

        IHttpRequestBuilder IHttpRequestAuthBuilder.WithNoAuthorizationHeader()
            => this;

        async Task<TResponse> IHttpRequestBuilder.PostAsync<TResponse>(string endpoint, object body)
        {
            var json = body == null? string.Empty : GetBodyJSON(body);

            using (var content = new StringContent(json, _encoding, _contentType))
            {
                return await CreateRequest<TResponse>(() => _httpClient.PostAsync(endpoint, content));
            }
        }

        async Task<TResponse> IHttpRequestBuilder.GetAsync<TResponse>(string endpoint)
            => await CreateRequest<TResponse>(() => _httpClient.GetAsync(endpoint));

        private async Task<TResponse> CreateRequest<TResponse>(Func<Task<HttpResponseMessage>> httpClientCall)
        {          
            using (var response = await httpClientCall())
            {
                string responseData = await response.Content.ReadAsStringAsync();
                _httpClient.Dispose();

                return JsonConvert.DeserializeObject<TResponse>(responseData);
            }           
        }

        private string GetBodyJSON(object body)
        {
            var stringBody = body as string;

            return (stringBody != null) ? stringBody : JsonConvert.SerializeObject(body, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
        }        
    }

}