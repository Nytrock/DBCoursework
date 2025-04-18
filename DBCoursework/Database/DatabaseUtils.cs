using DBCoursework.Utils;
using Npgsql;

namespace DBCoursework.Database {
    public static class DatabaseUtils {
        private readonly static List<Type> _allModels = [];

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

        public static IEnumerable<Type> GetAllModels() {
            if (_allModels.Count == 0)
                GenerateAllModelsList();
            return _allModels;
        }

        private static void GenerateAllModelsList() {
            Type baseModel = typeof(BaseModel);

            foreach (var a in AppDomain.CurrentDomain.GetAssemblies())
                foreach (var t in a.GetTypes())
                    if (t.IsSubclassOf(baseModel))
                        _allModels.Add(t);
        }
    }
}
