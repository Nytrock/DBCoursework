using System.Text;

namespace DBCoursework.Utils {
    public static class StringUtils {
        public static string ToSnakeCase(this string text) {
            if (text == null)
                return string.Empty;

            if (text.Length < 2)
                return text.ToLowerInvariant();

            var builder = new StringBuilder();
            builder.Append(char.ToLowerInvariant(text[0]));
            for (int i = 1; i < text.Length; ++i) {
                char c = text[i];
                if (char.IsUpper(c)) {
                    builder.Append('_');
                    builder.Append(char.ToLowerInvariant(c));
                } else {
                    builder.Append(c);
                }
            }
            return builder.ToString();
        }
    }
}
