using System.Globalization;

namespace order.flow.bootstraper.configurations.constants;

public static class Constant
{
    private const string DefaultLanguage = "pt-BR";
    public static CultureInfo DefaultCulture = new CultureInfo(DefaultLanguage);
    private const string CodLanguage = "en-US";
    public static CultureInfo UsCulture = new CultureInfo(CodLanguage);
    public const string APLICATIONID = "AplicationId";
    public const string ID = "Id";
}