using Serilog;

namespace com.softpine.muvany.infrastructure.SharedServices;

/// <summary>
/// 
/// </summary>
public static class StaticLogger
{
    /// <summary>
    /// 
    /// </summary>
    public static void EnsureInitialized()
    {
        if (Log.Logger is not Serilog.Core.Logger)
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();
        }
    }
}
