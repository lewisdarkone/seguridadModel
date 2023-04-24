
using Microsoft.AspNetCore.Components;

namespace com.softpine.muvany.component.Tools;

public static class StaticValues
{
    public static bool IsLoading = false;
    public static int sizePage = 10;
    public static int ActualPage = 1;
    public static int TotalPages = 1;
    public static string HomePage = "/documentos";



    public static string GetQueryDefault()
    {
        return $"PageSize={StaticValues.sizePage}&PageNumber={StaticValues.ActualPage}";
    }

    
}
