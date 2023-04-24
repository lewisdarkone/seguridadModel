using Microsoft.Extensions.Configuration;
using System.Text.Json;
using Repository.API;
using com.softpine.muvany.models.ResponseModels.EndpointResponse;
using com.softpine.muvany.models.ResponseModels;
using com.softpine.muvany.models.Requests;

namespace com.softpine.muvany.clientservices;

public class EndpointService : IEndpointService
{
    private readonly string _errorMsj;
    private readonly IConfiguration _config;
    private readonly IRepository<Object> _repository;
    private readonly TokenSingleton _tokenSingleton;
    private string _token;

    public EndpointService(IRepository<Object> repository,
    IConfiguration configuration, TokenSingleton tokenSingleton)
    {
        _repository = repository;
        _config = configuration;
        _errorMsj = _config.GetValue<string>("internalErrorMsj");
        _tokenSingleton = tokenSingleton;

    }

    public async Task<UpdateEndpointsResponse?> AssignEndpointPermiso(CreateOrUpdateEndpointPermisoRequest assignEndpointRequest)
    {
        var response = new UpdateEndpointsResponse();
        _token = await _tokenSingleton.GetToken();
        try
        {
            var result = await _repository.Post(assignEndpointRequest,"endpoints/assign-permiso", _token);
            response.Code = ((int)result.StatusCode);
            response.Message = result.ReasonPhrase!;

            if (result.IsSuccessStatusCode)
                response.Data = JsonSerializer.Deserialize<UpdateEndPointData>(result.Content.ReadAsStream(),Tools.JsonOption());
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

    public async Task<CreateEndpointsResponse?> CreateEndpoints(CreateEndpointRequest endpointRequest)
    {
        var response = new CreateEndpointsResponse();
        _token = await _tokenSingleton.GetToken();
        try
        {
            var result = await _repository.Post(endpointRequest, "endpoints/create", _token);
            response.Code = ((int)result.StatusCode);
            response.Message = result.ReasonPhrase!;

            if (result.IsSuccessStatusCode)
                response.Data = JsonSerializer.Deserialize<CreateEndPointData>(result.Content.ReadAsStream(),Tools.JsonOption());
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

    public async Task<UpdateEndpointsResponse?> DeleteEndpoints(string endpointId)
    {
        var response = new UpdateEndpointsResponse();

        try
        {
            var result = await _repository.Post(null,""+endpointId, _token);

            if (result.IsSuccessStatusCode)
                response.Data = JsonSerializer.Deserialize<UpdateEndPointData>(result.Content.ReadAsStream(),Tools.JsonOption());
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

    public async Task<GetEndpointsResponse?> GetEndpoints(string query = "")
    {
        var response = new GetEndpointsResponse();
        _token = await _tokenSingleton.GetToken();

        try
        {
            var result = await _repository.Get(_token, "endpoints/get?", query);
            response.Code = ((int)result.StatusCode);
            response.Message = result.ReasonPhrase!;

            if (result.IsSuccessStatusCode)
                response.Data = JsonSerializer.Deserialize<GetEndPointData>(result.Content.ReadAsStream(),Tools.JsonOption());
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

    public async Task<UpdateEndpointsResponse?> UpdateEndpoints(UpdateEndpointRequest endpointRequest)
    {
        var response = new UpdateEndpointsResponse();
        _token = await _tokenSingleton.GetToken();

        try
        {
            var result = await _repository.Post(endpointRequest, "endpoints/update", _token);
            response.Code = ((int)result.StatusCode);
            response.Message = result.ReasonPhrase!;

            if (result.IsSuccessStatusCode)
                response.Data = JsonSerializer.Deserialize<UpdateEndPointData>(result.Content.ReadAsStream(),Tools.JsonOption());
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
