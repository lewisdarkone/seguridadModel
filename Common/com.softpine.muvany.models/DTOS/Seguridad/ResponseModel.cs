namespace com.softpine.muvany.models.DTOS
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public class ResponseModel<TModel>
    {
        /// <summary>
        /// resultado que se mostrarara en la respuesta del middleware
        /// </summary>
        public TModel Result { get; set; }
        /// <summary>
        /// mensaje de que se mostrarara en la respuesta del middleware
        /// </summary>
        public string ErrorMessage { get; private set; }
        /// <summary>
        /// Si hubo un error
        /// </summary>
        public bool HasError { get; set; }
        /// <summary>
        /// Estable los valores de error
        /// </summary>
        /// <param name="message"></param>
        public void SetErrorMessage(string message)
        {
            ErrorMessage = message;
            HasError = true;
        }
    }
}
