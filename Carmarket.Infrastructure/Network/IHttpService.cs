namespace Carmarket.Infrastructure.Network
{
    public interface IHttpService
    {
        /// <summary>
        /// Send HTTP request
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<HttpResponseMessage> SendAsync(HttpRequestMessage request);

        /// <summary>
        /// Send HTTP request
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken);

        /// <summary>
        /// Get response
        /// </summary>
        /// <typeparam name="T">type of response</typeparam>
        /// <param name="httpResponseMessage"></param>
        /// <returns></returns>
        Task<T> GetResponse<T>(HttpResponseMessage httpResponseMessage);
    }
}
