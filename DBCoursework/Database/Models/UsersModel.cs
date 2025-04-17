using Npgsql;

namespace DBCoursework.Database.Models {
    public class UsersModel : BaseModel {
        private int _id;
        private string _username;
        private string _slug;
        private string _password;
        private string _image;
        private DateTime _birthday;
        private string _email;
        private string _description;
        private string _phoneNumber;

        public override void Init(NpgsqlDataReader reader) {
            _id = reader.GetInt32(0);
            _username = reader.GetString(1);
            _slug = reader.SafeGetString(2);
            _password = reader.GetString(3);
            _image = reader.SafeGetString(4);
            _birthday = reader.SafeGetDateTime(5);
            _email = reader.SafeGetString(6);
            _phoneNumber = reader.SafeGetString(7);

            _fieldsValues = [
                _id, _username, _slug, _password, _image,
                _birthday, _email, _phoneNumber
            ];
            _fieldsNames = [
                "id", "username", "slug", "password", "image",
                "birthday", "email", "phoneNumber"
            ];
        }
    }
}
