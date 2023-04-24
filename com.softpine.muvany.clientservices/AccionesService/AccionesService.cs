using Microsoft.Extensions.Configuration;
using Repository.API;
using System.Text.Json;
using com.softpine.muvany.models.ResponseModels.AccionesResponse;
using com.softpine.muvany.models.Requests;
using com.softpine.muvany.models.ResponseModels;

namespace com.softpine.muvany.clientservices
{
    public class AccionesService : IAccionesService
    {
        private readonly IConfiguration _config;
        private readonly IRepository<Object> _repository;
        private readonly TokenSingleton _tokenSingleton;
        private string _token;

        public AccionesService(IRepository<Object> repository,
        IConfiguration configuration, TokenSingleton tokenSingleton)
        {
            _config = configuration;
            _repository = repository;
            _tokenSingleton = tokenSingleton;
        }

        public async Task<CreateAccionesResponse?> CreateAcciones(CreateAccionesRequest createAccionesRequest)
        {
            var response = new CreateAccionesResponse();
            _token = await _tokenSingleton.GetToken();
            try
            {
                var result = await _repository.Post(createAccionesRequest, "acciones/create", _token);
                response.Code = ((int)result.StatusCode);
                response.Message = result.ReasonPhrase!;

                if ( result.IsSuccessStatusCode )
                    response.Data = JsonSerializer.Deserialize<CreateAccionesData>(result.Content.ReadAsStream(), Tools.JsonOption());
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


        public async Task<DeleteAccionesResponse?> DeleteAccion(string actionId, string token)
        {
            var response = new DeleteAccionesResponse();            
            var result = await _repository.Delete(""+actionId,token);

            if (result.IsSuccessStatusCode)
                response.Data = JsonSerializer.Deserialize<BasicResponse>(result.Content.ReadAsStream(),Tools.JsonOption());
            else
                response.Exception = JsonSerializer.Deserialize<ExceptionResponse>(result.Content.ReadAsStream(),Tools.JsonOption());

            response.Code = ((int)result.StatusCode);
            response.Message = result.ReasonPhrase!;

            return response;

        }


        public async Task<GetAccionesResponse?> GetAcciones(string query="")
        {            
            var response = new GetAccionesResponse();
            _token = await _tokenSingleton.GetToken();

            try
            {
                var result = await _repository.Get(_token, "acciones/get?", query);
                response.Code = ((int)result.StatusCode);
                response.Message = result.ReasonPhrase!;

                if ( result.IsSuccessStatusCode )
                    response.Data = JsonSerializer.Deserialize<GetAccionesData>(result.Content.ReadAsStream(), Tools.JsonOption());
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


        public async Task<UpdateAccionesResponse?> UpdateAcciones(UpdateAccionesRequest updateAccionesRequest)
        {
            var response = new UpdateAccionesResponse();
            _token = await _tokenSingleton.GetToken();

            try
            {
                var result = await _repository.Put(updateAccionesRequest, "", _token);
                response.Code = ((int)result.StatusCode);
                response.Message = result.ReasonPhrase!;

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

    }
}
