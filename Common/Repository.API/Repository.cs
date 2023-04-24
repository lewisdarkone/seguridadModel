using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Components;

namespace Repository.API
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _config;
        private readonly string _appJsonType;
        private readonly NavigationManager _navManager;

        

        public Repository(IHttpClientFactory httpClientFactory, IConfiguration configuration, NavigationManager navigationManager) {

            _httpClientFactory = httpClientFactory;
            _httpClient = _httpClientFactory.CreateClient("muvany");
            _navManager = navigationManager;
            
            
            _config = configuration;
            _appJsonType = ConstantsValues.AppType;

            
        }
        private void CheckoutCode(int code)
        {
            if (code == 401 )
            {
                _navManager.NavigateTo(_navManager.BaseUri);
            }
        }
        
        public async Task<HttpResponseMessage> Delete(string path, string token)
        {
            if (!string.IsNullOrEmpty(token))
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage result = await _httpClient.DeleteAsync(path);
            
            CheckoutCode((int)result.StatusCode);
            return result;
        }

        public async Task<HttpResponseMessage> Get(string token, string path, string query = "")
        {
            if (!string.IsNullOrEmpty(token))
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            string finalurl = path;
            if (!string.IsNullOrEmpty(query)) finalurl += query;

            HttpResponseMessage result = await _httpClient.GetAsync(finalurl);
            CheckoutCode((int)result.StatusCode);
            return result;
        }

        public async Task<HttpResponseMessage> Post(TEntity request, string path, string token)
        {
            if(!string.IsNullOrEmpty(token))
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpContent? content = null;

            if (request != null) {
                string sendContent = JsonSerializer.Serialize(request);
                content = new StringContent(
                   sendContent,
                   Encoding.UTF8,
                   _appJsonType
                );
            }
            
            HttpResponseMessage result = await _httpClient.PostAsync(path, content);
            CheckoutCode((int)result.StatusCode);
            return result;
        }

        public async Task<HttpResponseMessage> Put(TEntity data, string path, string token)
        {
            if (!string.IsNullOrEmpty(token))
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpContent? content = null;
            if (data != null) {
                string sendContent = JsonSerializer.Serialize(data);
                content = new StringContent(
                   sendContent,
                   Encoding.UTF8,
                   _appJsonType
                );
            }            

            HttpResponseMessage result = await _httpClient.PutAsync(path, content);
            CheckoutCode((int)result.StatusCode);
            return result;
        }

        public async Task<HttpResponseMessage> Send(HttpRequestMessage data)
        {
            return await _httpClient.SendAsync(data);
        }
    }
}
