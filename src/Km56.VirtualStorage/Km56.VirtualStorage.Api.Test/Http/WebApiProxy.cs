using Microsoft.Win32.SafeHandles;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Runtime.InteropServices;

namespace km56.VirtualStorage.Api.Test.Http
{
    public class WebApiProxy : IWebApiProxy, IDisposable
    {
        private readonly HttpClient _httpClient;

        public WebApiProxy(string baseUri)
        {
            if (string.IsNullOrWhiteSpace(baseUri))
            {
                throw new ArgumentNullException(nameof(baseUri));
            }

            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(baseUri)
            };

            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<TResponse?> GetAsync<TResponse>(string url)
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync(url);

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                // Handle error cases here if required
                httpResponseMessage.EnsureSuccessStatusCode();
            }

            var response = await httpResponseMessage.Content.ReadFromJsonAsync<TResponse>();

            return response;
        }

        public async Task<TResponse?> PostAsync<TRequest, TResponse>(string url, TRequest data)
        { 
            HttpResponseMessage responseMessage = await _httpClient.PostAsJsonAsync(url, data);
            if (!responseMessage.IsSuccessStatusCode)
            {
                // Handle error cases here if needed
                responseMessage.EnsureSuccessStatusCode();
            }

            var response = await responseMessage.Content.ReadFromJsonAsync<TResponse>();

            return response;
        }

        #region IDispose

        private bool _disposedValue;
        private SafeHandle? _safeHandle = new SafeFileHandle(IntPtr.Zero, true);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _safeHandle?.Dispose();
                    _safeHandle = null;
                }

                _disposedValue = true;
            }
        }

        #endregion
    }
}
