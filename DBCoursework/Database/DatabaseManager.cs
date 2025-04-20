using DBCoursework.Utils;
using Microsoft.Extensions.Options;
using Npgsql;

namespace DBCoursework.Database {
    public class DatabaseManager {
        private static readonly List<string> _allTables = [
            "Users", "UserLogins", "Friends", "Communities", "Posts",
            "PostAttachments", "Comments", "Chats", "ChatMessages", "UserMessages"
        ];

        private readonly NpgsqlDataSource _dataSource;

        public DatabaseManager(IOptions<DatabaseSettings> settingsOption) {
            DatabaseSettings settings = settingsOption.Value;
            _dataSource = NpgsqlDataSource.Create(settings.ToConnectionString());
        }

        async public Task<List<TableRow>> GetAll(string tableName) {
            string command = $"SELECT * FROM {tableName.ToSnakeCase()}";

            NpgsqlCommand sqlCommand = _dataSource.CreateCommand(command);
            NpgsqlDataReader reader = await sqlCommand.ExecuteReaderAsync();

            List<TableRow> result = [];
            while (await reader.ReadAsync()) {
                TableRow table = new(reader);
                result.Add(table);
            }
            return result;
        }

        async public Task<TableRow> GetById(string tableName, int id) {
            string command = $"SELECT * FROM {tableName.ToSnakeCase()} WHERE id={id}";

            NpgsqlCommand sqlCommand = _dataSource.CreateCommand(command);
            NpgsqlDataReader reader = await sqlCommand.ExecuteReaderAsync();

            await reader.ReadAsync();
            TableRow model = new(reader);
            return model;
        }

        async public Task Insert(string tableName, TableRow newModel) {
            string insertString = newModel.GetStringForInsert();
            string command = $"INSERT INTO {tableName} {insertString}";

            NpgsqlCommand sqlCommand = _dataSource.CreateCommand(command);
            await sqlCommand.ExecuteNonQueryAsync();
        }

        async public Task Update(string tableName, TableRow newModel, int id) {
            string updates = newModel.GetStringForUpdate();
            string command = $"UPDATE {tableName} SET {updates} WHERE id = {id}";

            NpgsqlCommand sqlCommand = _dataSource.CreateCommand(command);
            await sqlCommand.ExecuteNonQueryAsync();
        }

        async public Task Delete(string tableName, int id) {
            string command = $"DELETE FROM {tableName} WHERE id = {id}";

            NpgsqlCommand sqlCommand = _dataSource.CreateCommand(command);
            await sqlCommand.ExecuteNonQueryAsync();
        }

        async public void DeleteManyToMany<T>(string tableName, string firstIdName, int firstId, string secondIdName, int secondId) {
            string command = $"DELETE FROM {tableName} WHERE {firstIdName} = {firstId} AND {secondIdName} = {secondId}";

            NpgsqlCommand sqlCommand = _dataSource.CreateCommand(command);
            await sqlCommand.ExecuteNonQueryAsync();
        }

        public static IEnumerable<string> GetAllTables() => _allTables;
    }
}
