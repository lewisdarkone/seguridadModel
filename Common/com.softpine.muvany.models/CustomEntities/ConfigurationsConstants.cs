namespace com.softpine.muvany.models.CustomEntities
{
    /// <summary>
    /// 
    /// </summary>
    public class ConfigurationsConstants
    {
        /// <summary>
        /// 
        /// </summary>
        public string NameAprobradorTemplateCreateRole { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string NameAdminRoleAprobradorTemplate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string EmailInternoFirma { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string NameAdminRole { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string PasswordUserInterInitial { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string PasswordUserExterInitial { get; set; }
        /// <summary>
        /// Indica los dias que se le dara para expiración a usuarios externos 
        /// </summary>
        public int DefaultExternalUserDaysExpired { get; set; }
        /// <summary>
        /// Limite de intentos erroneos permitido a usuarios externos
        /// </summary>
        public int RetryIncorrectPasswordLimit { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string SymbolsAcepted { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Environment { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ServerDevelopment { get; set; }
    }
}
