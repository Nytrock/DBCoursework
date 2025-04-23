using DBCoursework.Database;
using DBCoursework.Utils;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        public static IEnumerable<SelectListItem> GenerateSelectFromRows(this List<TableRow> rows, string columnName) {
            List<SelectListItem> result = [];
            foreach (TableRow row in rows) {
                int id = Convert.ToInt32(row.GetColumn("id"));
                string? name = Convert.ToString(row.GetColumn(columnName));
                result.Add(new(name, id.ToString()));
            }
            return result;
        }
    }
}
