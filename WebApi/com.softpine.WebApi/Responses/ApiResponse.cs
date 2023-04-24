using com.softpine.muvany.models.CustomEntities;

namespace Matrices.Api.Responses
{
    /// <summary>
    /// Clase creada para retornar la respuesta de los verbos
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ApiResponse<T>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="data"></param>
        public ApiResponse(T data)
        {
            Data = data;
        }

        /// <summary>
        /// Propiedad que almacena la información a retornar
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// Propiedad que almacena información de Metadata como pagesize, pagenumber, etc
        /// </summary>
        public Metadata? Meta { get; set; }

       /// <summary>
        /// Almacena información sobre el error
        /// </summary>
        public string Message { get; set; }

 
    }
}
