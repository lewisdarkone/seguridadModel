using System.Text.Json;

namespace com.softpine.muvany.clientservices;

internal static class Tools
{
    public static JsonSerializerOptions? JsonOption() => new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
}
