using com.softpine.muvany.models.Requests;
using com.softpine.muvany.models.ResponseModels;
using com.softpine.muvany.models.ResponseModels.UserResponses;
using com.softpine.muvany.models.Tools;
using Microsoft.Extensions.Configuration;
using Repository.API;
using System.Text.Json;

namespace com.softpine.muvany.clientservices
{
    public class UserService : IUserService
    {
        private readonly string _errorMsj;
        private readonly IConfiguration _config;
        private readonly IRepository<Object> _repository;
        private readonly TokenSingleton _tokenSingleton;
        private string _token;

        public UserService(IConfiguration configuration, IRepository<Object> repository,
            TokenSingleton tokenSingleton)
        {
            _repository = repository;
            _tokenSingleton = tokenSingleton;
            _config = configuration;
            _errorMsj = _config.GetValue<string>("internalErrorMsj");          
        }


        public async Task<ChangeUserStatusResponse?> ChangeUserStatus(ToggleUserStatusRequest chageUserStatusRequest)
        {
            var response = new ChangeUserStatusResponse();
            _token = await _tokenSingleton.GetToken();
            try
            {                
                var result = await _repository.Post(chageUserStatusRequest, "users/change-status", _token);
                response!.Code = ((int)result.StatusCode);
                response.Message = result.ReasonPhrase;

                if ( result.IsSuccessStatusCode )
                    response.Data =
                        JsonSerializer.Deserialize<BasicResponse>(result.Content.ReadAsStream(), Tools.JsonOption());
                else
                    response.Exception =
                        JsonSerializer.Deserialize<ExceptionResponse>(result.Content.ReadAsStream(),
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

        public async Task<ConfirmEmailResponse?> ConfirmUserEmail(string query)
        {
            var response = new ConfirmEmailResponse();
            _token = await _tokenSingleton.GetToken();

            try
            {
                if ( string.IsNullOrEmpty(query) )
                {
                    return new ConfirmEmailResponse()
                    {
                        Code = 400,
                        Message = "No se encontraron los parametros esperados"
                    };
                }

                var result = await _repository.Get(_token, "users/confirm-email", query);
                response!.Code = ((int)result.StatusCode);
                response.Message = result.ReasonPhrase!;

                if ( result.IsSuccessStatusCode )
                    response.Data =
                        JsonSerializer.Deserialize<BasicResponse>(result.Content.ReadAsStream(), Tools.JsonOption());
                else
                    response.Exception =
                        JsonSerializer.Deserialize<ExceptionResponse>(result.Content.ReadAsStream(), Tools.JsonOption());
                
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

        public async Task<CreateUserResponse?> CreateUser(CreateUserRequest userRequest)
        {
            var response = new CreateUserResponse();
            _token = await _tokenSingleton.GetToken();

            try
            {
                var result = await _repository.Post(userRequest, "users/create", _token);
                response!.Code = ((int)result.StatusCode);
                response.Message = result.ReasonPhrase;

                if ( result.IsSuccessStatusCode )
                    response.Data = JsonSerializer.Deserialize<BasicResponse>(result.Content.ReadAsStream(), Tools.JsonOption());
                else
                    response.Exception = JsonSerializer.Deserialize<ExceptionResponse>(result.Content.ReadAsStream(), Tools.JsonOption());
                
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

        public async Task<GetUserResponse?> GetUserbyId(string id)
        {
            var response = new GetUserResponse();
            _token = await _tokenSingleton.GetToken();

            try
            {
                var result = await _repository.Get(_token, "users/get/" + id);
                response!.Code = ((int)result.StatusCode);
                response.Message = result.ReasonPhrase;

                if ( result.IsSuccessStatusCode )
                    response.Data = JsonSerializer.Deserialize<UserData>(result.Content.ReadAsStream(), Tools.JsonOption());
                else
                    response.Exception =
                        JsonSerializer.Deserialize<ExceptionResponse>(result.Content.ReadAsStream(), Tools.JsonOption());                
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

        public async Task<GetRolesResponse?> GetUserRoles(string userId)
        {
            var response = new GetRolesResponse();
            _token = await _tokenSingleton.GetToken();

            try
            {
                var result = await _repository.Get(_token, $"Users/get/{userId}/roles");

                if ( result.IsSuccessStatusCode )
                    response.Data =
                        JsonSerializer.Deserialize<UserRolesData>(result.Content.ReadAsStream(), Tools.JsonOption());
                else
                    response.Exception =
                        JsonSerializer.Deserialize<ExceptionResponse>(result.Content.ReadAsStream(), Tools.JsonOption());

                response!.Code = ((int)result.StatusCode);
                response.Message = result.ReasonPhrase!;
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

        public async Task<GetUsersResponse?> GetUsers(string query = "")
        {
            var response = new GetUsersResponse();
            _token = await _tokenSingleton.GetToken();

            try
            {
                var result = await _repository.Get(_token, "users/get?", query);
                response.Code = ((int)result.StatusCode);
                response.Message = result.ReasonPhrase;

                if ( result.IsSuccessStatusCode )
                    response.Data =
                        JsonSerializer.Deserialize<UserListResponse>(result.Content.ReadAsStream(), Tools.JsonOption());
                else
                    response.Exception =
                        JsonSerializer.Deserialize<ExceptionResponse>(result.Content.ReadAsStream(), Tools.JsonOption());

                
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

        public async Task<UpdateUserResponse?> UpdateUser(UpdateUserRequest updateUserRequest)
        {
            var response = new UpdateUserResponse();
            _token = await _tokenSingleton.GetToken();

            try
            {
                var result = await _repository.Put(updateUserRequest, "users/update", _token);
                response!.Code = ((int)result.StatusCode);
                response.Message = result.ReasonPhrase!;

                if ( result.IsSuccessStatusCode )
                    response.Data =
                        JsonSerializer.Deserialize<BasicResponse>(result.Content.ReadAsStream(), Tools.JsonOption());
                else
                    response.Exception =
                        JsonSerializer.Deserialize<ExceptionResponse>(result.Content.ReadAsStream(), Tools.JsonOption());

                
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

        public async Task<UpdateRoleResponse?> UpdateUserRoles(string userId, UserRolesRequest userRoles)
        {
            var response = new UpdateRoleResponse();
            _token = await _tokenSingleton.GetToken();
            try
            {
                var result = await _repository.Put(userRoles, $"users/update/{userId}/roles", _token);
                response!.Code = ((int)result.StatusCode);
                response.Message = result.ReasonPhrase;

                if ( result.IsSuccessStatusCode )
                    response.Data =
                        JsonSerializer.Deserialize<BasicResponse>(result.Content.ReadAsStream(), Tools.JsonOption());
                else
                    response.Exception =
                        JsonSerializer.Deserialize<ExceptionResponse>(result.Content.ReadAsStream(), Tools.JsonOption());                
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

        public async Task<ForgotPasswordResponse?> ForgotPassword(string email)
        {
            var response = new ForgotPasswordResponse();
            _token = await _tokenSingleton.GetToken();
            try
            {
                var result = await _repository.Post(null, "users/forgot-password?Email="+email,_token);
                response!.Code = ((int)result.StatusCode);
                response.Message = result.ReasonPhrase!;

                if ( result.IsSuccessStatusCode )
                    response.Data =
                        JsonSerializer.Deserialize<ForgotPasswordData>(result.Content.ReadAsStream(), Tools.JsonOption());
                else
                    response.Exception =
                        JsonSerializer.Deserialize<ExceptionResponse>(result.Content.ReadAsStream(), Tools.JsonOption());

                
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

        public async Task<ResetPasswordResponse?> ResetPassword(ResetPasswordRequest resetPasswordRequest)
        {
            var response = new ResetPasswordResponse();

            try
            {
                var result = await _repository.Post(resetPasswordRequest, "users/reset-password","");
                response!.Code = ((int)result.StatusCode);
                response.Message = result.ReasonPhrase!;

                if ( result.IsSuccessStatusCode )
                    response.Data =
                        JsonSerializer.Deserialize<BasicResponse>(result.Content.ReadAsStream(), Tools.JsonOption());
                else
                    response.Exception =
                        JsonSerializer.Deserialize<ExceptionResponse>(result.Content.ReadAsStream(), Tools.JsonOption());                

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

        public async Task<GetUserDomainResponse?> GetUserDomain(string email)
        {
            var response = new GetUserDomainResponse();
            _token = await _tokenSingleton.GetToken();

            try
            {
                var result = await _repository.Get(_token, "usersdomain/get/" + email);
                response.Code = ((int)result.StatusCode);
                response.Message = result.ReasonPhrase;

                if ( result.IsSuccessStatusCode )
                    response.Data =
                        JsonSerializer.Deserialize<GetUserDomainData>(result.Content.ReadAsStream(), Tools.JsonOption());
                else
                    response.Exception =
                        JsonSerializer.Deserialize<ExceptionResponse>(result.Content.ReadAsStream(), Tools.JsonOption());
                
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

        public async Task<UpdateResponse> RegisterUser(RegisterUserRequest request)
        {
            var response = new UpdateResponse();
            _token = await _tokenSingleton.GetToken();

            try
            {
                var result = await _repository.Post(request, "users/register", _token);
                response!.Code = (int)result.StatusCode;
                response.Message = result.ReasonPhrase;

                if ( result.IsSuccessStatusCode )
                    response.Data =   JsonSerializer.Deserialize<BasicResponse>(result.Content.ReadAsStream(), Tools.JsonOption());
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
    }
}
