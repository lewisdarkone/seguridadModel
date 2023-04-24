using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace com.softpine.muvany.infrastructure.Filters
{
    /// <summary>
    /// 
    /// </summary>
    public class ValidationFilter : IAsyncActionFilter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.Values.Where(v => v.Errors.Count > 0)
                   .SelectMany(v => v.Errors)
                   .Select(v => v.ErrorMessage)
                   .ToList();

                

                context.Result = new JsonResult(errors)
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                };

                return;
            }
    await next();
        }
    }
}
