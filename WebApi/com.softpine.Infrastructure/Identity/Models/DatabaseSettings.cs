namespace com.softpine.muvany.infrastructure.Identity.Models;

/// <summary>
/// 
/// </summary>
public class DatabaseSettings
{
    /// <summary>
    /// 
    /// </summary>
    public string? DBProvider { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string? ConnectionString { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string? ConnectionStringTemplateGrafs { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string? ConnectionStringDBLogs { get; set; }
    /// <summary>
    /// Base de datos en mongo DB
    /// </summary>
    public string ConnectionStringMongoDb { get; set; }
    /// <summary>
    /// SuscripcionDb
    /// </summary>
    public string SuscripcionDatabase { get; set; }
}
