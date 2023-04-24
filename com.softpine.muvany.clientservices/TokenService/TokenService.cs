using com.softpine.muvany.models.Requests;
using com.softpine.muvany.models.ResponseModels;
using com.softpine.muvany.models.ResponseModels.TokenResponse;
using Microsoft.Extensions.Configuration;
using Repository.API;
using System.Text.Json;

namespace com.softpine.muvany.clientservices
{
    
    public class TokenService : ITokenService
    {
        private readonly string _errorMsj;
        private readonly string _tokenPath;
        private readonly string _tokenRefreshPath;
        private readonly IConfiguration _config;
        private readonly IRepository<Object> _repository;

        public TokenService(IRepository<Object> repository,
        IConfiguration configuration)
        {
            _config = configuration;
            _errorMsj = _config.GetValue<string>("internalErrorMsj");
            _tokenRefreshPath = _config.GetValue<string>("tokeRefreshPath");
            _repository = repository;
        }

        public async Task<GetTokenResponse> GetByCodeToken(TokenByCodeRequest getTokenResquest)
        {
            var response = new GetTokenResponse();
            try
            {
                var result = await _repository.Post(getTokenResquest, "tokens/validatecode", "");
                response!.Code = ((int)result.StatusCode);
                response.Message = result.ReasonPhrase;

                if ( result.IsSuccessStatusCode )
                    response.Data = JsonSerializer.Deserialize<GetTokenData>(result.Content.ReadAsStream(), Tools.JsonOption());
                else
                    response.Exception = JsonSerializer.Deserialize<ExceptionResponse>(result.Content.ReadAsStream(), Tools.JsonOption());


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

        public async Task<GetTokenResponse?> Login(TokenRequest getTokenResquest)
        {
            var response = new GetTokenResponse();
            try
            {
                var result = await _repository.Post(getTokenResquest, "tokens", "");
                response!.Code = ((int)result.StatusCode);
                response.Message = result.ReasonPhrase;

                if ( result.IsSuccessStatusCode )
                    response.Data = JsonSerializer.Deserialize<GetTokenData>(result.Content.ReadAsStream(),Tools.JsonOption());
                else
                    response.Exception = JsonSerializer.Deserialize<ExceptionResponse>(result.Content.ReadAsStream(),Tools.JsonOption());

               
            }
            catch ( Exception ex)
            {
                response.Exception = new();
                response.Exception.statusCode = response.Code;
                response.Exception.messages = new string[] { response.Message };
                await Console.Out.WriteLineAsync($"Error: {ex.Message}");
            }

            return response;
        }

        public async Task<GetTokenResponse?> RefreshToken(RefreshTokenRequest getTokenResquest)
        {
            var response = new GetTokenResponse();

            try
            {
                var result = await _repository.Post(getTokenResquest, "", "");
                response!.Code = ((int)result.StatusCode);
                response.Message = result.ReasonPhrase;

                if (result.IsSuccessStatusCode)
                    response.Data = JsonSerializer.Deserialize<GetTokenData>(result.Content.ReadAsStream(),Tools.JsonOption());
                else
                    response.Exception = JsonSerializer.Deserialize<ExceptionResponse>(result.Content.ReadAsStream(),Tools.JsonOption());
            }
            catch (Exception ex)
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
