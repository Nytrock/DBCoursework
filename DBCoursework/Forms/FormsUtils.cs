using DBCoursework.Database;
using DBCoursework.Utils;

namespace DBCoursework.Forms {
    public static class FormsUtils {
        public static void GetDataFromTableRow(this BaseForm form, TableRow tableRow, Type formType) {
            foreach (var property in formType.GetProperties()) {
                if (property.PropertyType == typeof(IEnumerable<int>))
                    continue;

                object propertyValue = tableRow.GetColumn(property.Name.ToSnakeCase());
                if (propertyValue is DBNull)
                    continue;
                property.SetValue(form, propertyValue);
            }
        }

        public static void SetDataToTableRow(this BaseForm form, TableRow tableRow, Type formType, bool isNewRow = false) {
            foreach (var property in formType.GetProperties()) {
                if (property.PropertyType == typeof(IEnumerable<int>))
                    continue;

                object? value = property.GetValue(form);
                if (value is DateTime && property.Name.StartsWith("Creation") && isNewRow)
                    continue;

                tableRow.SetColumn(property.Name.ToSnakeCase(), value);
            }
        }
    }
}
