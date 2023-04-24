using com.softpine.muvany.core.Exceptions;
using com.softpine.muvany.core.Interfaces;
using com.softpine.muvany.models.Constants;
using com.softpine.muvany.models.Interfaces;
using Microsoft.AspNetCore.Http;
using Serilog;
using Serilog.Context;
using System.Net;
using System.Text;

namespace com.softpine.muvany.infrastructure.Shared.Middleware;

internal class ExceptionMiddleware : IMiddleware
{
    private readonly ICurrentUser _currentUser;
    private readonly ISerializerService _jsonSerializer;
    private readonly IBaseRepositoryDapper _repositoryDapper;

    public ExceptionMiddleware(
        ICurrentUser currentUser,
        ISerializerService jsonSerializer,
        IBaseRepositoryDapper repositoryDapper)
    {
        _currentUser = currentUser;
        _jsonSerializer = jsonSerializer;
        _repositoryDapper = repositoryDapper;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch ( Exception exception )
        {
            string email = _currentUser.GetUserEmail() is string userEmail ? userEmail : "Anonymous";
            var userId = _currentUser.GetUserId();

            if ( userId != string.Empty )
                LogContext.PushProperty("UserId", userId);
            LogContext.PushProperty("UserEmail", email);

            string errorId = Guid.NewGuid().ToString();
            LogContext.PushProperty("ErrorId", errorId);
            LogContext.PushProperty("StackTrace", exception.StackTrace);

            var errorResult = new ErrorResult
            {
                Source = exception.TargetSite?.DeclaringType?.FullName,
                ErrorId = errorId,
                SupportMessage = $"{ApiConstants.Messages.SupportMessage} {errorId}"
            };

            errorResult.Messages.Add(exception.Message);

            // String builder para exception.Message
            StringBuilder exceptionMessage = new StringBuilder();
            exceptionMessage.Append(exception.Message);

            // String builder para exception.StackTrace
            StringBuilder exceptionStackTrace = new StringBuilder();
            exceptionStackTrace.Append(exception.StackTrace);

            var innerException = exception.InnerException;
            if ( exception is not CustomException && exception is not BusinessException)
            {
                while ( innerException != null )
                {
                    exceptionMessage.Append(innerException.Message);
                    exceptionStackTrace.Append(innerException.StackTrace);

                    innerException = innerException.InnerException;
                }

                errorResult.Exception = exceptionMessage.ToString().Trim();

                var mainLog = new
                {
                    StackTrace = exceptionStackTrace.ToString().Trim(),
                    Source = errorResult.Source,
                    Message = errorResult.Exception,
                    LevelExceptionId = 1,
                    ApplicationLogId = 4,
                    Created = DateTime.Now,
                    UserName = email != String.Empty ? email : "Anonymous",
                    MachineName = Environment.MachineName,
                    Code = 100,
                    ErrorId = errorResult.ErrorId
                };
                //todo: configurar insercion de error en mongo
                //var logResult = await _repositoryDapper.Insert("Tbl_Logs_MainLogs", mainLog);
            }



            switch ( exception )
            {
                case CustomException e:
                    errorResult.StatusCode = (int)e.StatusCode;
                    if ( e.ErrorMessages is not null )
                    {
                        errorResult.Messages = e.ErrorMessages;
                    }

                    break;

                case KeyNotFoundException:
                    errorResult.StatusCode = (int)HttpStatusCode.NotFound;
                    break;
                case BusinessException:
                    errorResult.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
                    break;
                default:
                    errorResult.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            var response = context.Response;
            if ( !response.HasStarted )
            {
                response.ContentType = "application/json";
                response.StatusCode = errorResult.StatusCode;
                await response.WriteAsync(_jsonSerializer.Serialize(errorResult));
            }
            else
            {
                Log.Warning("Can't write error response. Response has already started.");
            }
        }
    }
}
