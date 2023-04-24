using com.softpine.muvany.models.Requests;

namespace com.softpine.muvany.models.Interfaces.InterfacesServices
{
    /// <summary>
    /// 
    /// </summary>
    public interface IImagenService : ITransientService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="imagen"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        Task<ImagenRequest> OptimizarImagenAsync(ImagenRequest imagen, int width, int height = 0);
    }
}
