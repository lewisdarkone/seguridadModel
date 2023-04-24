using Microsoft.Extensions.Configuration;
using System.Text.Json;
using Repository.API;
using com.softpine.muvany.models.Requests;
using com.softpine.muvany.models.ResponseModels.PersonalResponse;
using com.softpine.muvany.models.ResponseModels;

namespace com.softpine.muvany.clientservices
{
    public class PersonalService : IPersonalService
    {
        private readonly IRepository<Object> _repository;
        private readonly TokenSingleton _tokenSingleton;
        private string _token;

        public PersonalService(IRepository<Object> repository,
        IConfiguration configuration, TokenSingleton tokenSingleton)
        {
            _repository = repository;
            _tokenSingleton = tokenSingleton;
        }

        public async Task<ChangePasswordResponse> ChangePassword(ChangePasswordRequest changePasswordRequest)
        {
            var response = new ChangePasswordResponse();
            _token = await _tokenSingleton.GetToken();
            try
            {
                var result = await _repository.Put(changePasswordRequest,"personal/change-password", _token);
                response.Code = ((int)result.StatusCode);
                response.Message = result.ReasonPhrase!;

                if (result.IsSuccessStatusCode)
                    response.Data = JsonSerializer.Deserialize<BasicResponse>(result.Content.ReadAsStream(),Tools.JsonOption());
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

        public async Task<GetPermissionsResponse?> GetPermissions()
        {
            var response = new GetPermissionsResponse();
            _token = await _tokenSingleton.GetToken();
            try
            {
                var result = await _repository.Get(_token, "personal/permissions");
                response.Code = ((int)result.StatusCode);
                response.Message = result.ReasonPhrase!;

                if (result.IsSuccessStatusCode)
                    response.Data = JsonSerializer.Deserialize<GetPermissionsData>(result.Content.ReadAsStream(),Tools.JsonOption());
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

        public async Task<ProfileResponse> GetProfile()
        {
            var response = new ProfileResponse();
            _token = await _tokenSingleton.GetToken();
            try
            {
                var result = await _repository.Get(_token, "personal/profile");
                response!.Code = ((int)result.StatusCode);
                response.Message = result.ReasonPhrase!;

                if (result.IsSuccessStatusCode)
                    response.Data = JsonSerializer.Deserialize<ProfileData>(result.Content.ReadAsStream(),Tools.JsonOption());
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

        public async Task<UpdateProfileResponse> UpdateProfile(UpdateUserProfileRequest updateProfileRequest)
        {
            var response = new UpdateProfileResponse();
            _token = await _tokenSingleton.GetToken();
            try
            {
                var result = await _repository.Put(updateProfileRequest, "personal/profile", _token);
                response.Code = ((int)result.StatusCode);
                response.Message = result.ReasonPhrase!;

                if (result.IsSuccessStatusCode)
                    response.Data = JsonSerializer.Deserialize<BasicResponse>(result.Content.ReadAsStream(),Tools.JsonOption());
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
