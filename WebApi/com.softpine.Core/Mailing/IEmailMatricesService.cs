using com.softpine.muvany.models.Interfaces;

namespace com.softpine.muvany.core.Interfaces;

/// <summary>
/// 
/// </summary>
public interface IEmailMatricesService : ITransientService
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="templateName"></param>
    /// <param name="mailTemplateModel"></param>
    /// <returns></returns>
    string GenerateEmailTemplate<T>(string templateName, T mailTemplateModel);
}
