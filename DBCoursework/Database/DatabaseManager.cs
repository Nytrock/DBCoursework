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

        public async Task<List<TableRow>> ReadAll(string tableName) {
            string command = $"SELECT * FROM {tableName.ToSnakeCase()}";
            return await ReadTableRows(command);
        }

        public async Task<TableRow> ReadById(string tableName, int id) {
            string command = $"SELECT * FROM {tableName.ToSnakeCase()} WHERE id={id}";
            return (await ReadTableRows(command))[0];
        }

        public async Task<int> Create(string tableName, TableRow newModel) {
            string insertString = newModel.GetStringForInsert();
            string command = $"INSERT INTO {tableName} {insertString}";
            return await ExecuteCreateCommand(command);
        }

        public async Task Update(string tableName, TableRow newModel, int id) {
            string updates = newModel.GetStringForUpdate();
            string command = $"UPDATE {tableName} SET {updates} WHERE id = {id}";
            await ExecuteCommand(command);
        }

        public async Task Delete(string tableName, int id) {
            string command = $"DELETE FROM {tableName} WHERE id = {id}";
            await ExecuteCommand(command);
        }

        public async Task<List<int>> ReadManyToManyById(string tableName, string mainIdName, int mainIdValue, string secondIdName) {
            mainIdName = mainIdName.ToSnakeCase();
            secondIdName = secondIdName.ToSnakeCase();
            tableName = tableName.ToSnakeCase();

            string command = $"SELECT * FROM {tableName} WHERE {mainIdName} = {mainIdValue}";
            List<TableRow> rows = await ReadTableRows(command);
            return rows.Select(row => Convert.ToInt32(row.GetColumn(secondIdName))).ToList();
        }

        public async void UpdateManyToMany(string tableName, string firstIdName, int firstId, string secondIdName, IEnumerable<int>? secondIds) {
            tableName = tableName.ToSnakeCase();
            firstIdName = firstIdName.ToSnakeCase();
            secondIdName = secondIdName.ToSnakeCase();

            string command = $"SELECT * FROM {tableName} WHERE {firstIdName}={firstId}";
            List<TableRow> tableRows = await ReadTableRows(command);
            secondIds ??= [];
            List<int> oldSecondIds = [];

            foreach (TableRow tableRow in tableRows) {
                int oldSecondId = Convert.ToInt32(tableRow.GetColumn(secondIdName));
                oldSecondIds.Add(oldSecondId);
                if (secondIds.Contains(oldSecondId))
                    continue;

                DeleteManyToMany(tableName, firstIdName, firstId, secondIdName, oldSecondId);
            }

            foreach (var secondId in secondIds) {
                if (oldSecondIds.Contains(secondId))
                    continue;
                CreateManyToMany(tableName, firstIdName, firstId, secondIdName, secondId);
            }
        }

        public async void DeleteManyToMany(string tableName, string firstIdName, int firstId, string secondIdName, int secondId) {
            string command = $"DELETE FROM {tableName} WHERE {firstIdName} = {firstId} AND {secondIdName} = {secondId}";
            await ExecuteCommand(command);
        }

        public async void CreateManyToMany(string tableName, string firstIdName, int firstId, string secondIdName, int secondId) {
            string command = $"INSERT INTO {tableName} ({firstIdName}, {secondIdName}) VALUES ({firstId}, {secondId})";
            await ExecuteCommand(command);
        }

        private async Task<List<TableRow>> ReadTableRows(string command) {
            NpgsqlCommand sqlCommand = _dataSource.CreateCommand(command);
            NpgsqlDataReader reader = await sqlCommand.ExecuteReaderAsync();

            List<TableRow> result = [];
            while (await reader.ReadAsync()) {
                TableRow table = new(reader);
                result.Add(table);
            }
            return result;
        }

        private async Task<int> ExecuteCreateCommand(string command) {
            command += " returning id";
            NpgsqlCommand sqlCommand = _dataSource.CreateCommand(command);
            var result = await sqlCommand.ExecuteScalarAsync();
            Console.WriteLine(result);
            return Convert.ToInt32(result);
        }

        private async Task ExecuteCommand(string command) {
            NpgsqlCommand sqlCommand = _dataSource.CreateCommand(command);
            await sqlCommand.ExecuteNonQueryAsync();
        }

        public static IEnumerable<string> GetAllTables() => _allTables;
    }
}
