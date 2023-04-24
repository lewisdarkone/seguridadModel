using com.softpine.muvany.models.Enumerations;
using com.softpine.muvany.models.Requests;

namespace com.softpine.muvany.models.Interfaces;

/// <summary>
/// 
/// </summary>
public interface IFileStorageService : ITransientService
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="request"></param>
    /// <param name="supportedFileType"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<string> UploadAsync<T>(FileUploadRequest? request, FileType supportedFileType, CancellationToken cancellationToken = default)
    where T : class;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="path"></param>
    public void Remove(string? path);
}
