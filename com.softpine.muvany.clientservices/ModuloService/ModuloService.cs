using Microsoft.Extensions.Configuration;
using System.Text.Json;
using Repository.API;
using com.softpine.muvany.models.Request.RequestsIdentity;
using com.softpine.muvany.models.ResponseModels;
using com.softpine.muvany.models.ResponseModels.ModuloResponse;

namespace com.softpine.muvany.clientservices
{
    public class ModuloService : IModuloService
    {
        private readonly string _errorMsj;
        private readonly IConfiguration _config;
        private readonly IRepository<Object> _repository;
        private readonly TokenSingleton _tokenSingleton;
        private string _token;

        private readonly string _deleteModuloPath;


        public ModuloService(IRepository<Object> repository,
        IConfiguration configuration, TokenSingleton tokenSingleton)
        {
            _repository = repository;
            _config = configuration;
            _tokenSingleton = tokenSingleton;

            _errorMsj = _config.GetValue<string>("internalErrorMsj");

            _deleteModuloPath = _config.GetValue<string>("deleteModuloPath");


        }

        public async Task<ModuloResponse?> Add(CreateModulosRequest addModuloRequest)
        {
            var response = new ModuloResponse();
            _token = await _tokenSingleton.GetToken();
            try
            {
                var result = await _repository.Post(addModuloRequest, "modulos/create", _token);
                response.Code = ((int)result.StatusCode);
                response.Message = result.ReasonPhrase!;

                if (result.IsSuccessStatusCode)
                    response.done = true;
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

        public async Task<GetModulosResponse> GetAll(string? query="")
        {
            var response = new GetModulosResponse();
            _token = await _tokenSingleton.GetToken();
            try
            {
                var result = await _repository.Get(_token,"modulos/get?",query);
                response.Code = ((int)result.StatusCode);
                response.Message = result.ReasonPhrase!;

                if (result.IsSuccessStatusCode)
                    response.Data = JsonSerializer.Deserialize<GetAllModuloData>(result.Content.ReadAsStream(),Tools.JsonOption());
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

        public async Task<UpdateResponse?> Update(UpdateModulosRequest modulo)
        {
            var response = new UpdateResponse();
            _token = await _tokenSingleton.GetToken();
            try
            {
                var result = await _repository.Put(modulo,"modulos/update", _token);
                response.Message = result.ReasonPhrase!;
                response.Code = ((int)result.StatusCode);

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

        public async Task<UpdateResponse?> Delete(string moduloId)
        {
            var response = new UpdateResponse();
            _token = await _tokenSingleton.GetToken();
            try
            {
                var result = await _repository.Delete(_deleteModuloPath + moduloId,_token);

                if (result.IsSuccessStatusCode)
                    response.Data = JsonSerializer.Deserialize<BasicResponse>(result.Content.ReadAsStream(),Tools.JsonOption());
                else
                    response.Exception = JsonSerializer.Deserialize<ExceptionResponse>(result.Content.ReadAsStream(),Tools.JsonOption());

                response.Code = (int)result.StatusCode;
                response.Message = result.ReasonPhrase!;
            }
            catch (Exception ex)
            {
                response.Code = 500;
                response.Message = $"No fue posible contactar el servicio: {ex.Message}";

            }
            return response;
        }
    }
}
