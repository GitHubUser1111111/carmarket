using Newtonsoft.Json;

namespace Carmarket.Infrastructure.Network.Implementations
{
    public class HttpService : IHttpService
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly JsonSerializer jsonSerializer;

        public HttpService(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
            this.jsonSerializer = new JsonSerializer();
        }

        public async Task<T> GetResponse<T>(HttpResponseMessage httpResponseMessage)
        {
            using var stream = await httpResponseMessage.Content.ReadAsStreamAsync();
            using var streamReader = new StreamReader(stream);
            using var jsonTextReader = new JsonTextReader(streamReader);

            return jsonSerializer.Deserialize<T>(jsonTextReader);
        }

        public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
        {
            var client = httpClientFactory.CreateClient();

            var response = await client.SendAsync(request);

            return response;
        }

        public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var client = httpClientFactory.CreateClient();

            var response = await client.SendAsync(request, cancellationToken);

            return response;
        }
    }
}
