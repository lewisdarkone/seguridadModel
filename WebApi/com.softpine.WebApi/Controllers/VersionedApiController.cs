using Microsoft.AspNetCore.Mvc;

namespace com.softpine.muvany.WebApi.Controllers;

/// <summary>
/// 
/// </summary>
[Route("api/v{version:apiVersion}/[controller]")]
public class VersionedApiController : BaseApiController
{
}
