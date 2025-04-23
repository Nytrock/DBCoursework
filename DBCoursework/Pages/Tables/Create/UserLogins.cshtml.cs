using DBCoursework.BaseModels;
using DBCoursework.Database;
using DBCoursework.Forms;
using DBCoursework.Forms.Tables;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DBCoursework.Pages.Tables.Create {
    public class UserLoginsModel(DatabaseManager databaseManager, FormsManager formsManager) : CreateTableModel<UserLoginsForm>(databaseManager, formsManager) {
        public IEnumerable<SelectListItem>? OriginalUser { get; set; }

        protected override async Task LoadKeysValues() {
            List<TableRow> posts = await _databaseManager.ReadAll("Users");
            OriginalUser = posts.GenerateSelectFromRows("username");
        }
    }
}
