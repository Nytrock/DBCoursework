namespace DBCoursework.Database {
    public class DatabaseSettings {
        public string Host { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }

        public string ToConnectionString() {
            return $"Host={Host};Username={Username};Password={Password};Database={Name}";
        }
    }
}
