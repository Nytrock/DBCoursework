using Microsoft.Extensions.Options;
using Npgsql;

namespace DBCoursework.Database {
    public class DatabaseManager {
        private readonly NpgsqlDataSource _dataSource;

        public DatabaseManager(IOptions<DatabaseSettings> settingsOption) {
            DatabaseSettings settings = settingsOption.Value;
            _dataSource = NpgsqlDataSource.Create(settings.ToConnectionString());
        }

        async public IAsyncEnumerable<T> GetAll<T>() where T : BaseModel, new() {
            string modelName = DatabaseUtils.GetModelName<T>();
            string command = $"SELECT * FROM {modelName}";

            NpgsqlCommand sqlCommand = _dataSource.CreateCommand(command);
            NpgsqlDataReader reader = await sqlCommand.ExecuteReaderAsync();

            while (await reader.ReadAsync()) {
                T model = new();
                model.Init(reader);
                yield return model;
            }
        }

        async public Task<T> GetById<T>(int id) where T : BaseModel, new() {
            string modelName = DatabaseUtils.GetModelName<T>();
            string command = $"SELECT * FROM {modelName} WHERE id={id}";

            NpgsqlCommand sqlCommand = _dataSource.CreateCommand(command);
            NpgsqlDataReader reader = await sqlCommand.ExecuteReaderAsync();

            T model = new();
            model.Init(reader);
            return model;
        }

        async public void Insert<T>(T newModel) where T : BaseModel {
            string modelName = DatabaseUtils.GetModelName<T>();
            string insertString = newModel.GetStringForInsert();
            string command = $"INSERT INTO {modelName} {insertString}";

            NpgsqlCommand sqlCommand = _dataSource.CreateCommand(command);
            await sqlCommand.ExecuteNonQueryAsync();
        }

        async public void Update<T>(T newModel, int id) where T : BaseModel {
            string modelName = DatabaseUtils.GetModelName<T>();
            string updates = newModel.GetStringForUpdate();
            string command = $"UPDATE {modelName} SET {updates} WHERE id = {id}";

            NpgsqlCommand sqlCommand = _dataSource.CreateCommand(command);
            await sqlCommand.ExecuteNonQueryAsync();
        }

        async public void Delete<T>(int id) where T : BaseModel {
            string modelName = DatabaseUtils.GetModelName<T>();
            string command = $"DELETE FROM {modelName} WHERE id = {id}";

            NpgsqlCommand sqlCommand = _dataSource.CreateCommand(command);
            await sqlCommand.ExecuteNonQueryAsync();
        }

        async public void DeleteManyToMany<T>(string firstIdName, int firstId, string secondIdName, int secondId) where T : BaseModel {
            string modelName = DatabaseUtils.GetModelName<T>();
            string command = $"DELETE FROM {modelName} WHERE {firstIdName} = {firstId} AND {secondIdName} = {secondId}";

            NpgsqlCommand sqlCommand = _dataSource.CreateCommand(command);
            await sqlCommand.ExecuteNonQueryAsync();
        }
    }
}
