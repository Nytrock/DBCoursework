using Npgsql;
using System.Text;

namespace DBCoursework.Database {
    public class TableRow {
        protected Dictionary<string, object> _columns;

        public TableRow() {
            _columns = [];
        }

        public TableRow(NpgsqlDataReader reader) {
            _columns = [];

            var columns = reader.GetColumnSchema();
            for (int i = 0; i < columns.Count; i++) {
                string columnName = columns[i].ColumnName;
                object columnValue = reader.GetValue(i);
                _columns.Add(columnName, columnValue);
            }
        }

        public string GetStringForInsert() {
            StringBuilder builder = new();
            builder.Append('(');

            List<string?> values = [];
            List<object> names = [];
            foreach (var field in _columns) {
                if (!field.IsTableColumnWritable())
                    continue;

                values.Add(field.Value.ToCommandFormat());
                names.Add(field.Key);
            }

            builder.Append(String.Join(", ", names));
            builder.Append(") VALUES (");
            builder.Append(String.Join(", ", values));

            builder.Append(')');
            return builder.ToString();
        }

        public string GetStringForUpdate() {
            StringBuilder builder = new();

            List<string> updates = [];
            foreach (var field in _columns) {
                if (!field.IsTableColumnWritable())
                    continue;
                updates.Add($"{field.Key} = {field.Value.ToCommandFormat()}");
            }
            builder.Append(String.Join(", ", updates));

            return builder.ToString();
        }

        public object GetColumn(string columnName) {
            return _columns[columnName];
        }

        public void SetColumn(string columnName, object? value) {
            if (DatabaseUtils.IsTableColumnEmpty(value))
                return;

            _columns[columnName] = value;
        }

        public IEnumerable<KeyValuePair<string, object>> GetColumns() {
            foreach (var column in _columns)
                yield return column;
        }
    }
}
