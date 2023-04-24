#pragma warning disable 1591
namespace com.softpine.muvany.models.Constants;

/// <summary>
/// 
/// </summary>
public static class ApiConstants
{
    /// <summary>
    /// 
    /// </summary>
    public static class Messages
    {

        /// Authenticaton and Authorization
        public const string AuthenticationFailed = "Usuario no autenticado";
        public const string UnauthorizedAccess = "Usuario no autorizado para acceder al recurso";
        public const string ForbiddenAccess = "Usuario no autorizado para acceder al recurso";
        public const string ExpiredToken = "Token expirado";
        public const string InvalidToken = "Token inválido";
        public const string NotDefinedTokenKey = "Token key no definido en configuraciones de JWT";
        public const string UserOrEmailNotValid = "Usuario no autenticado";
        public const string UserNotValid = "Usuario no autenticado";
        public const string UserExist = "Usuario existe, utilice otro email";
        public const string ExpiredAccount = "Cuenta expirada, por favor comunicarse con el administrador";

        /// Users and Roles
        public const string UserNotActive = "El usuario no se encuentra activo. Por favor contactar al administrador";
        public const string UserNotFound = "Usuario no encontrado";
        public const string RoleNotFound = "Rol no encontrado";
        public const string NotDisableYourUser = "No puede desactivar su usuario";

        /// Email Confirmation
        public const string EmailNotConfirmed = "E-mail no confirmado";
        public const string EmailConfirmationError = "Error en confirmación de email";
        public const string EmailConfirmed_1 = "Cuenta confirmada para el email ";
        public const string EmailConfirmed_2 = "Ahora se puede usar el endpoint /api/tokens para generar el JWT.";
        public const string EmailConfirmed_3 = "Debe confirmar su cuenta";

        /// MobilePhone Confirmation
        public const string MobilePhoneConfirmationError = "Error en confirmación de número telefónico";
        public const string MobilePhoneConfirmed_1 = "Cuenta confirmada para el número telefónico ";
        public const string MobilePhoneConfirmed_2 = "Ahora se puede usar el endpoint /api/tokens para generar el JWT.";
        public const string MobilePhoneConfirmed_3 = "Debe confirmar su E-mail antes de usar el endpoint /api/tokens para generar el JWT";

        /// Validations
        public const string UsernameTaken = "El username ya está registrado";
        public const string ValidationCodeExpired = "El código esta expirado, por favor introduzca un codigo nuevo";
        public const string ValidationCodeIncorrect = "El código es incorrecto, por favor verifique su correo y pueba de nuevo";
        public const string EmailTaken = "El email ya está registrado";
        public const string FileFormatNotSupported = "Formato de archivo no soportado";
        public const string NameIsRequired = "Name es requerido";
        public const string InvalidId = "Esta enviando un parametro con un Id inválido";
        public const string WrongTipoTasacion = "El Tipo Tasación no corresponde al proceso de tasación al que quiere acceder";
        public const string RequestNotEditByState = "La solicitud no puede ser editada en el estado actual";
        public const string RequestNotWorkByState = "La solicitud no puede ser trabajada en el estado actual";
        public const string ParameterCantPhotoSupplierNotRegister = "El parámetro de suplidor con la cantidad de fotos NO esta registrado";
        public const string ValidateCantPhotoSend = "La cantidad de fotos enviadas supera la cantidad solicitada por el suplidor";
        public const string CantPhotosSendInvalid = "La cantidad de fotos enviadas es menor que la cantidad solicitada por el suplidor";
        public const string PhotosChasisSendInvalid = "Debe existir 1 foto Tipo Chasis";
        public const string DateRequestDiferentDatePhoto = "La fecha de las fotos difiere de la fecha de la solicitud";
        public const string RequestByIdNoEmpty = "Debe enviar el Número de Solicitud ó el Número de Tasación";
        public const string RoleNotAvailableForUser = "No puede asignar roles internos a usuarios externos y viceversa";
        public const string NotAvailableByState = "No puede ejecutar esta acción por el estatus actual en que se encuentra el proceso";
        public const string NotValueAccept = "No puede enviar valores vacíos, nulos o en cero ";
        public const string NotFilterFound = "No hay registros con el valor con el/los parámetro/os enviados ";
        public const string NotPhotoFound = "No existe una imagen del perfil agregada al usuario";
        public const string NotFirmaFound = "No existe una imagen de firma agregada al usuario";
        public const string ExistSimilarRequestDB = "Existe una solicitud creada y en proceso con los mismos datos";
        public const string ExistInDB = "El registro que intenta crear ya existe";
        public const string NotParameterSend = "No se enviaron los parámetros solicitados para la consulta";
        public const string NotCompletedByValorate = "No se han completado todos los datos necesarios para la valoración";
        public const string NotCompletedAccessoryByTasacion = "Debe de guardar los accesorios con al menos uno seleccionado.";
        public const string NoDisponible = "No Disponible";
        public const string DiferentPhotoDateForValidation = "La fecha de las fotos es diferente a la fecha en la que se realiza la valoración";
        public const string NoFacturaNotCero = "El Número de factura no es válido";
        public const string NCFnotValid = "El Número de Comprobante Fiscal no es válido";
        public const string NotValidParameter = "Los parámetros enviados no son válidos";
        public const string NotValidEdition = "Debe colocar una edición válida";
        public const string UserNotAllowedOperation = "Operación no permitida para este usuario";
        public const string SupplierNotHaveRate = "El suplidor no tiene una tarifa asignada para el costo de las Template.";

        /// General CRUD
        public const string CreationFailed = "Error en registro";
        public const string UpdateNotAllowed = "Actualización no permitida";
        public const string UpdateFailed = "Error en actualización";
        public const string DeletionNotAllowed = "Eliminación no permitida";
        public const string NoteVinWithNoRelationRequest = "Las informaciones de la consulta VIN difieren de la Solicitud de Crédito. Se conservan los datos de la Solicitud de Crédito.";
        public const string NoteNotFoundDataInApiVin = "No se encontraron datos en el servicio Decodificador VIN. Se conservan los datos de la Solicitud de Crédito.";

        /// Password
        public const string ChangePasswordFailed = "Error en cambio de contraseña";
        public const string FormatPasswordFailed = "La constraseña debe tener al menos 8 dígitos y tener al menos una Mayúscula, Minúscula, Número, Símbolo [!\"·$%&/()=¿¡?'_:;,|@#€*+.]";
        public const string ConfirmPassAndPassNotEquals = "La contraseña y la confirmación de contraseña son diferentes";

        /// Permisos
        public const string PermissionNotFound = "Permiso no encontrado";
        public const string UserNotSupplierError = "El Usuario no tiene un suplidor asignado";

        /// Endpoints
        public const string EndpointNotFound = "Endpoint no encontrado";

        /// General Errors
        public const string ServerError = "Error en servidor";
        public const string ValidationErrors = "Uno o más errores de validación han ocurrido";
        public const string DataEmptyError = "Data vacía";
        public const string DeserializeError = "Error de deserialización";
        public const string PropertyExpressionError = "propertyExpr debe ser una property expression.";
        public const string InScopeInitializationReservedError = "Método reservado para inicialización in-scope";
        public const string UnexpectedHashFormatError = "Formato de hash no soportado";
        public const string VinNotFound = "Información del chasis no encontrada";
        public const string VinLenghtError = "El tamaño del VIN no corresponde con el permitido";
        public const string VinDataErrorTitulo = "Discrepancia consulta VIN con datos Solicitud";
        public const string VinDataError = "Las informaciónes de la consulta VIN difieren de la Solicitud de Crédito";
        public const string ShouldSendParameters = "Debe de enviar todos los parámetros necesarios.";


        /// Generic Errors
        public const string Title_400 = "Petición inválida.";
        public const string Title_500 = "Error interno.";
        public const string Title_409 = "Conflicto.";
        public const string Title_401 = "No autorizado.";

        /// Default Messages
        public const string SupportMessage = "Para un mayor análisis, puede proporcionar al equipo de soporte el error id:";

        /// Template
        public const string DataByfilterNotFound = "No existen datos con los filtros enviados";
        public const string CreditRequestNotExists = "La solicitud no existe";
        public const string WrongTypeSolicitud = "El número de tasación digitado no es del tipo de tasación solicitado";
        public const string WrongStateSolicitudToUpdate = "No se puede actualizar la solicitud en el estado actual ";

        // Adjuntos
        public const string TypePathError = "El Tipo de Adjunto enviado no corresponde con el servicio que está utilizando";

        // Reportes
        public const string ReporteTasacionInvalid = "Solo se permiten los reportes en Solicitudes Tipo Tasación(Crédito y Incautado) y Estado Tasación(Valoradas).";
        public const string ReporteFacturaInvalid = "Solo se permiten los reportes de Facturas en Estado Aprobadas.";

        public const string RegistryNotFoundWithId = "No se encontraron registros con el ID: ";
    }

    public static class ContentTypes
    {
        public const string ProblemJson = "application/problem+json";
        public const string ProblemXml = "application/problem+xml";
    }

    public static class HttpStatuses
    {
        public const string BadRequest = "https://httpstatuses.com/400";
        public const string NotFound = "https://httpstatuses.com/404";
        public const string InternalServerError = "https://httpstatuses.com/500";
    }

    public static class ValidateUsersDomainVariables
    {
        /// Validate Users Domain
        public const string sDomain = @"dominio";
        public const string sServiceUser = @"usuarioservicio";
        public const string sServicePassword = "claveUsuarioServicio";
    }
    public static class ParametersGenerics
    {
        public const string FormatFechsString = "yyyy-MM-dd";
    }

}



