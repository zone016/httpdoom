namespace HttpDoom.Utilities
{
    internal static class StringExtensions
    {
        public static string RemoveSchema(this string uri) =>
            uri
                .Replace("http://", string.Empty)
                .Replace("https://", string.Empty);
    }
}