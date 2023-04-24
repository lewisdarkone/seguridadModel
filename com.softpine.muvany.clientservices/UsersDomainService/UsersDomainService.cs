using System.Text.Json;
using Blazored.LocalStorage;
using com.softpine.muvany.models.ResponseModels;
using com.softpine.muvany.models.ResponseModels.UsersDomainResponse;
using Microsoft.Extensions.Configuration;
using Repository.API;

namespace com.softpine.muvany.clientservices.UsersDomainService
{
    public class UsersDomainService : IUsersDomainService
    {
        private readonly string _errorMsj;
        private readonly IConfiguration _config;
        private readonly IRepository<object> _repository;
        private readonly ILocalStorageService _localStorage;
        private string _token;
        private string _basePath;


        public UsersDomainService(IRepository<object> repository,
            IConfiguration configuration, ILocalStorageService localStorage)
        {
            _repository = repository;
            _config = configuration;
            _errorMsj = _config.GetValue<string>("internalErrorMsj");
            _localStorage = localStorage;
            _basePath = _config.GetValue<string>("getUserDomainPath");
        }

        async Task<string> GetToken()
        {
            return _token = !string.IsNullOrEmpty(_token)
                ? _token
                : await _localStorage.GetItemAsync<string>("UserToken");
        }

        public async Task<GetUsersDomainResponse?> Get(string query = "")
        {
            var response = new GetUsersDomainResponse();
            _token = await GetToken();
            try
            {
                var result = await _repository.Get(_token, $"{_basePath}", query);
                response.Code = (int)result.StatusCode;
                response.Message = result.ReasonPhrase!;

                if ( result.IsSuccessStatusCode )
                    response.Data =
                        JsonSerializer.Deserialize<GetUsersDomainData>(await result.Content.ReadAsStreamAsync(),
                            Tools.JsonOption());
                else
                    response.Exception =
                        JsonSerializer.Deserialize<ExceptionResponse>(await result.Content.ReadAsStreamAsync(),
                            Tools.JsonOption());

                
            }
            catch ( Exception ex )
            {
                response.Exception = new();
                response.Exception.statusCode = response.Code;
                response.Exception.messages = new string[] { response.Message };
                await Console.Out.WriteLineAsync($"Error: {ex.Message}");
            }

            return response;
        }

        public async Task<GetUsersDomainByIdResponse?> GetByEmail(string email)
        {
            var response = new GetUsersDomainByIdResponse();
            _token = await GetToken();
            try
            {
                var result = await _repository.Get(_token, $"{_basePath}/{email}");
                response.Code = (int)result.StatusCode;
                response.Message = result.ReasonPhrase!;

                if ( result.IsSuccessStatusCode )
                    response.Data =
                        JsonSerializer.Deserialize<GetUsersDomainByIdData>(await result.Content.ReadAsStreamAsync(),
                            Tools.JsonOption());
                else
                    response.Exception =
                        JsonSerializer.Deserialize<ExceptionResponse>(await result.Content.ReadAsStreamAsync(),
                            Tools.JsonOption());

                
            }
            catch ( Exception ex )
            {
                response.Exception = new();
                response.Exception.statusCode = response.Code;
                response.Exception.messages = new string[] { response.Message };
                await Console.Out.WriteLineAsync($"Error: {ex.Message}");
            }

            return response;
        }
    }
}
