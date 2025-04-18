using DBCoursework.Forms.Tables;

namespace DBCoursework.Forms {
    public class FormsManager {
        public BaseForm? GetFormForTable(string tableName) {
            switch (tableName) {
                case "Users":
                    return new UsersForm();
                default:
                    return null;
            }
        }
    }
}
