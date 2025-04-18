using DBCoursework.Database;
using DBCoursework.Utils;

namespace DBCoursework.Forms {
    public static class FormsUtils {
        public static void GetDataFromTableRow(this BaseForm form, TableRow tableRow, Type formType) {
            foreach (var property in formType.GetProperties()) {
                object propertyValue = tableRow.GetColumn(property.Name.ToSnakeCase());
                if (propertyValue is DBNull)
                    continue;
                property.SetValue(form, propertyValue);
            }
        }

        public static void SetDataToTableRow(this BaseForm form, TableRow tableRow, Type formType) {
            foreach (var property in formType.GetProperties())
                tableRow.SetColumn(property.Name.ToSnakeCase(), property.GetValue(form));
        }
    }
}
