using Microsoft.Extensions.Configuration;
using System.Text;
using System.Text.Json;
using Repository.API;
using com.softpine.muvany.models.ResponseModels.RoleClaims;
using com.softpine.muvany.models.ResponseModels;
using com.softpine.muvany.models.Requests;

namespace com.softpine.muvany.clientservices
{
    public class RolesClaimsService : IRolesClaimsService
    {
        private readonly string _errorMsj;
        private readonly IConfiguration _config;
        private readonly IRepository<Object> _repository;
        private readonly TokenSingleton _tokenSingleton;
        private string _token;

        private readonly string _getRolesClaimsPath;
        private readonly string _asignRolesClaimsPath;
        private readonly string _updateRolesClaimsPath;
        private readonly string _deleteRolesClaimsPath;

        public RolesClaimsService(IRepository<Object> repository,
        IConfiguration configuration, TokenSingleton tokenSingleton)
        {
            _repository = repository;
            _config = configuration;
            _tokenSingleton = tokenSingleton;
            _errorMsj = _config.GetValue<string>("internalErrorMsj");

        }

        public async Task<RoleClaimsResponse?> AssingRoleClaims(CreateRolePermissionsRequest assingRoleClaimRequest)
        {
            var response = new RoleClaimsResponse();
            _token = await _tokenSingleton.GetToken();
            try
            {
                var result = await _repository.Post(assingRoleClaimRequest,_asignRolesClaimsPath, _token);
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

        public async Task<RoleClaimsResponse?> DeleteRoleClaims(CreateRolePermissionsRequest assingRoleClaimRequest)
        {
            string request = JsonSerializer.Serialize(assingRoleClaimRequest);
            _token = await _tokenSingleton.GetToken();
            var response = new RoleClaimsResponse();

            var deleteRequest = new HttpRequestMessage {
                Method = HttpMethod.Delete,
                RequestUri = new Uri(_deleteRolesClaimsPath),
                Content = new StringContent(
                    request,
                    Encoding.UTF8,
                    "")};

            try
            {
                var result = await _repository.Send(deleteRequest);
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

        public async Task<GetRolClaimsListResponse?> GetRoleClaims(string query = "")
        {
            var response = new GetRolClaimsListResponse();
            _token = await _tokenSingleton.GetToken();
            try
            {
                var result = await _repository.Get(_token,_getRolesClaimsPath,query);
                response!.Code = ((int)result.StatusCode);
                response.Message = result.ReasonPhrase!;

                if (result.IsSuccessStatusCode)
                    response.Data = JsonSerializer.Deserialize<GetRolClaimsListResponseData>(result.Content.ReadAsStream(),Tools.JsonOption());
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

        public async Task<GetRoleClaimsPermissionResponse?> GetRoleClaimsPermission(string id)
        {            
            var response = new GetRoleClaimsPermissionResponse();
            _token = await _tokenSingleton.GetToken();
            try
            {
                var result = await _repository.Get(_token,$"rolesclaims/get/{id}");
                response.Code = ((int)result.StatusCode);
                response.Message = result.ReasonPhrase!;

                if (result.IsSuccessStatusCode)
                    response.Data = JsonSerializer.Deserialize<GetRoleClaimsPermisionData>(result.Content.ReadAsStringAsync().Result,Tools.JsonOption());
                //response = Newtonsoft.Json.JsonConvert.DeserializeObject<GetRoleClaimsPermissionResponse>(result.Content.ReadAsStringAsync().Result);
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


        public async Task<RoleClaimsResponse?> UpdateRoleClaims(CreateRolePermissionsRequest assingRoleClaimRequest)
        {
            var response = new RoleClaimsResponse();
            _token = await _tokenSingleton.GetToken();
            try
            {
                var result = await _repository.Put(assingRoleClaimRequest, "rolesclaims/update", _token);
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
