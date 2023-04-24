using Microsoft.Extensions.Configuration;
using System.Text.Json;
using Repository.API;
using com.softpine.muvany.models.ResponseModels.RoleResponse;
using com.softpine.muvany.models.Requests;
using com.softpine.muvany.models.ResponseModels;

namespace com.softpine.muvany.clientservices
{
    public class RoleService : IRoleService
    {
        private readonly IConfiguration _config;
        private readonly IRepository<Object> _repository;
        private readonly TokenSingleton _tokenSingleton;
        private string _token;
        private readonly string _errorMsj;

        public RoleService(IRepository<Object> repository,
        IConfiguration configuration, TokenSingleton tokenSingleton) 
        {
            _repository = repository;
            _tokenSingleton = tokenSingleton;
            _config = configuration;
            
            _errorMsj = _config.GetValue<string>("internalErrorMsj");

        }

        public async Task<CreateRoleResponse?> CreateRole(CreateRoleRequest createRoleRequest)
        {
            var response = new CreateRoleResponse();
            _token = await _tokenSingleton.GetToken();
            try
            {
                var result = await _repository.Post(createRoleRequest, "roles/create", _token);

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

        public async Task<DeleteRoleResponse?> DeleteRole(string roleId)
        {
            var response = new DeleteRoleResponse();
            _token = await _tokenSingleton.GetToken();
            try
            {
                var result = await _repository.Delete("" + roleId,_token);
                response.Code = (int)result.StatusCode;
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

        public async Task<GetRoleByIdResponse?> GetRoleById(string roleId)
        {
            var response = new GetRoleByIdResponse();
            _token = await _tokenSingleton.GetToken();
            try
            {
                var result = await _repository.Get(_token, "roles/get/" + $"{roleId}");
                response.Code = ((int)result.StatusCode);
                response.Message = result.ReasonPhrase!;

                if (result.IsSuccessStatusCode)
                    response.Data = JsonSerializer.Deserialize<GetRoleByIdData>(result.Content.ReadAsStream(),Tools.JsonOption());
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

        public async Task<GetRolesListResponse?> GetRoles(string query = "")
        {
            var response = new GetRolesListResponse();
            _token = await _tokenSingleton.GetToken();
            try
            {
                var result = await _repository.Get(_token, "roles/get?", query);
                response.Code = ((int)result.StatusCode);
                response.Message = result.ReasonPhrase!;

                if (result.IsSuccessStatusCode)
                    response.Data = JsonSerializer.Deserialize<GetRoleListData>(result.Content.ReadAsStream(),Tools.JsonOption());
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

        public async Task<CreateRoleResponse?> UpdateRole(UpdateRoleRequest updateRoleRequest)
        {
            CreateRoleResponse? response = new CreateRoleResponse();
            _token = await _tokenSingleton.GetToken();
            try
            {
                var result = await _repository.Put(updateRoleRequest,"roles/update", _token);
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
