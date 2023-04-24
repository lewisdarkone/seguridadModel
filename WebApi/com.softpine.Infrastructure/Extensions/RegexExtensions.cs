using System.Text.RegularExpressions;

namespace com.softpine.muvany.infrastructure.Extensions;

/// <summary>
/// 
/// </summary>
public static class RegexExtensions
{
    private static readonly Regex Whitespace = new(@"\s+");

    /// <summary>
    /// 
    /// </summary>
    /// <param name="input"></param>
    /// <param name="replacement"></param>
    /// <returns></returns>
    public static string ReplaceWhitespace(this string input, string replacement)
    {
        return Whitespace.Replace(input, replacement);
    }
}
