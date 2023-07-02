using System.Text.RegularExpressions;

namespace ColegioMirim.Core.Extensions
{
    public static class StringExtensions
    {
        public static string ApenasNumeros(this string text)
        {
            return Regex.Replace(text, @"\D", string.Empty);
        }

        public static string TrimMiddle(this string text)
        {
            return Regex.Replace(text, @"(?<=[^\s])\s+(?=[^\s])", " ");
        }

        public static string ApenasCaracteresEspeciais(this string text)
        {
            return Regex.Replace(text, @"[0-9a-zA-Z]", string.Empty);
        }
    }
}
