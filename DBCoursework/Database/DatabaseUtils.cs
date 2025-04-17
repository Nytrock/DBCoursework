using DBCoursework.Utils;
using Npgsql;

namespace DBCoursework.Database {
    public static class DatabaseUtils {
        public static string GetModelName<T>() where T : BaseModel {
            string camelCaseName = typeof(T).Name.Replace("Model", "");
            return camelCaseName.ToSnakeCase();
        }

        public static string SafeGetString(this NpgsqlDataReader reader, int columnIndex) {
            if (reader.IsDBNull(columnIndex))
                return string.Empty;
            return reader.GetString(columnIndex);
        }

        public static DateTime SafeGetDateTime(this NpgsqlDataReader reader, int columnIndex) {
            if (reader.IsDBNull(columnIndex))
                return DateTime.MinValue;
            return reader.GetDateTime(columnIndex);
        }

        public static bool IsModelFieldEmpty(this object field) {
            if (field is string)
                return string.IsNullOrEmpty(field.ToString());

            if (field is DateTime)
                return Convert.ToDateTime(field) == DateTime.MinValue;

            return false;
        }
    }
}
