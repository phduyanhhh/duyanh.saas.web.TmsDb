using DuyAnh.SaaS.Debugging;

namespace DuyAnh.SaaS;

public class SaaSConsts
{
    public const string LocalizationSourceName = "SaaS";
    public const string DefaultSchema = "Tourism";

    public const string ConnectionStringName = "Default";

    public const bool MultiTenancyEnabled = true;


    /// <summary>
    /// Default pass phrase for SimpleStringCipher decrypt/encrypt operations
    /// </summary>
    public static readonly string DefaultPassPhrase =
        DebugHelper.IsDebug ? "gsKxGZ012HLL3MI5" : "e067dbc7273d417ba4d25298a189bbd6";
}
