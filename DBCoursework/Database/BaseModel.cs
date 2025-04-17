using Npgsql;
using System.Text;

namespace DBCoursework.Database {
    public abstract class BaseModel {
        protected object[] _fieldsValues;
        protected string[] _fieldsNames;

        public abstract void Init(NpgsqlDataReader reader);

        public string GetStringForInsert() {
            StringBuilder builder = new();
            builder.Append('(');

            List<object> values = [];
            List<object> names = [];
            for (int i = 0; i < _fieldsNames.Length; i++) {
                if (_fieldsValues[i].IsModelFieldEmpty())
                    continue;

                values.Add(_fieldsValues[i]);
                names.Add(_fieldsNames[i]);
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
            for (int i = 0; i < _fieldsValues.Length; i++) {
                if (_fieldsValues[i].IsModelFieldEmpty())
                    continue;
                updates.Add($"{_fieldsNames[i]} = {_fieldsValues[i]}");
            }
            builder.Append(String.Join(", ", updates));

            return builder.ToString();
        }
    }
}
