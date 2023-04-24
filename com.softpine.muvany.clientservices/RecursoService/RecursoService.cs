using Microsoft.Extensions.Configuration;
using System.Text.Json;
using Repository.API;
using com.softpine.muvany.models.ResponseModels.RecursoResponse;
using com.softpine.muvany.models.Requests;
using com.softpine.muvany.models.ResponseModels;
using Newtonsoft.Json.Linq;

namespace com.softpine.muvany.clientservices;

public class RecursoService : IRecursoService
{
    private readonly string _errorMsj;
    private readonly IConfiguration _config;
    private readonly IRepository<Object> _repository;
    private readonly TokenSingleton _tokenSingleton;
    private string _token;

    private readonly string _deletePath;

    public RecursoService(IRepository<Object> repository,
    IConfiguration configuration, TokenSingleton tokenSingleton)
    {
        _repository = repository;
        _config = configuration;
        _tokenSingleton = tokenSingleton;
        _errorMsj = _config.GetValue<string>("internalErrorMsj");
        _deletePath = _config.GetValue<string>("deleteRecursoPath");
    }

    public async Task<CreateRecursoResponse?> Create(CreateRecursosRequest createRecursoRequest)
    {
        var response = new CreateRecursoResponse();
        _token = await _tokenSingleton.GetToken();
        try
        {
            var result = await _repository.Post(createRecursoRequest, "recursos/create", _token);
            response.Code = ((int)result.StatusCode);
            response.Message = result.ReasonPhrase!;

            if (result.IsSuccessStatusCode)
                response.Data = JsonSerializer.Deserialize<CreateRecursoData>(result.Content.ReadAsStream(),Tools.JsonOption());
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

    public async Task<DeleteRecursoResponse?> Delete(string recursoId)
    {
        var response = new DeleteRecursoResponse();

        try
        {
            var result = await _repository.Delete(_deletePath + recursoId,_token);

            if (result.IsSuccessStatusCode)
                response.Data = JsonSerializer.Deserialize<BasicResponse>(result.Content.ReadAsStream(),Tools.JsonOption());
            else
                response.Exception = JsonSerializer.Deserialize<ExceptionResponse>(result.Content.ReadAsStream(),Tools.JsonOption());

            response.Code = ((int)result.StatusCode);
            response.Message = result.ReasonPhrase!;

        }
        catch (Exception ex)
        {
            response.Code = 500;
            response.Message = _errorMsj + "\n" + ex.Message;
        }
        return response;
    }

    public async Task<GetRecursosResponse?> Get(string query = "")
    {
        var response = new GetRecursosResponse();
        _token = await _tokenSingleton.GetToken();
        try
        {
            var result = await _repository.Get(_token,"recursos/get?",query);
            response.Code = ((int)result.StatusCode);
            response.Message = result.ReasonPhrase!;

            if (result.IsSuccessStatusCode)
                response.Data = JsonSerializer.Deserialize<GetRecursosData>(result.Content.ReadAsStream(),Tools.JsonOption());
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

    public async Task<GetMenuResponse> GetMenu()
    {
        var response = new GetMenuResponse();
        _token = await _tokenSingleton.GetToken();

        try
        {
            var result = await _repository.Get(_token,"recursos/get/menu");
            response.Code = ((int)result.StatusCode);
            response.Message = result.ReasonPhrase!;

            if (result.IsSuccessStatusCode)
                response.Data = JsonSerializer.Deserialize<GetMenuData>(result.Content.ReadAsStream(),Tools.JsonOption());
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

    public async Task<UpdateResponse?> Update(UpdateRecursosRequest recurso)
    {
        var response = new UpdateResponse();
        _token = await _tokenSingleton.GetToken();
        try
        {
            var result = await _repository.Put(recurso, "recursos/update", _token);
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
