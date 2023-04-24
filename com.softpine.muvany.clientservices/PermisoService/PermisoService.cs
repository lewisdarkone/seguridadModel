using Microsoft.Extensions.Configuration;
using System.Text.Json;
using Repository.API;
using com.softpine.muvany.models.Requests;
using com.softpine.muvany.models.ResponseModels.PermisoResponse;
using com.softpine.muvany.models.ResponseModels;

namespace com.softpine.muvany.clientservices
{
    public class PermisoService : IPermisoService
    {
        private readonly string _errorMsj;
        private readonly IConfiguration _config;
        private readonly IRepository<Object> _repository;
        private readonly TokenSingleton _tokenSingleton;
        private string _token;

        private readonly string _deletePermisosPath;

        public PermisoService(IRepository<Object> repository,
        IConfiguration configuration, TokenSingleton tokenSingleton)
        {
            _repository = repository;
            _config = configuration;
            _tokenSingleton = tokenSingleton;
            _errorMsj = _config.GetValue<string>("internalErrorMsj");

            _deletePermisosPath = _config.GetValue<string>("deletePermisosPath");
        }

        public async Task<CreatePermisoResponse?> CreatePermiso(CreateOrUpdatePermisoRequest createPermisoRequest)
        {
            var response = new CreatePermisoResponse();
            _token = await _tokenSingleton.GetToken();

            try
            {
                var result = await _repository.Post(createPermisoRequest,"permisos/create", _token);

                response.Code = ((int)result.StatusCode);
                response.Message = result.ReasonPhrase!;

                if (result.IsSuccessStatusCode)
                    response.Data = JsonSerializer.Deserialize<CreateDataResponse>(result.Content.ReadAsStream(),Tools.JsonOption());
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

        public async Task<PermisoResponse?> DeletePermiso(string permisoId)
        {
            var response = new PermisoResponse();

            try
            {
                var result = await _repository.Delete(_deletePermisosPath+permisoId, _token);

                response.Done = result.IsSuccessStatusCode;
                response.Code = ((int)result.StatusCode);
                response.Message = result.ReasonPhrase!;

                if (!result.IsSuccessStatusCode)
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

        public async Task<GetPermisosResponse?> GetPermisos(string query = "")
        {
            var response = new GetPermisosResponse();
            _token = await _tokenSingleton.GetToken();
            try
            {
                var result = await _repository.Get(_token,"permisos/get?", query);

                response.Code = ((int)result.StatusCode);
                response.Message = result.ReasonPhrase!;

                if (result.IsSuccessStatusCode)
                    response.Data = JsonSerializer.Deserialize<GetPermisoData>(result.Content.ReadAsStream(),Tools.JsonOption());
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

        public async Task<PermisoResponse?> UpdatePermiso(CreateOrUpdatePermisoRequest createPermisoRequest)
        {
            var response = new PermisoResponse();
            _token = await _tokenSingleton.GetToken();
            try
            {
                var result = await _repository.Put(createPermisoRequest,"permisos/update", _token);

                response.Done = result.IsSuccessStatusCode;
                response.Code = ((int)result.StatusCode);
                response.Message = result.ReasonPhrase!;

                if (!result.IsSuccessStatusCode)
                    response.Exception = JsonSerializer.Deserialize<ExceptionResponse>(result.Content.ReadAsStream(), Tools.JsonOption());               

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
