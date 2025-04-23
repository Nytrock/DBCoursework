using DBCoursework.BaseModels;
using DBCoursework.Database;
using DBCoursework.Forms;
using DBCoursework.Forms.Tables;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DBCoursework.Pages.Tables.Create {
    public class ChatMessagesModel(DatabaseManager databaseManager, FormsManager formsManager) : CreateTableModel<ChatMessagesForm>(databaseManager, formsManager) {
        public IEnumerable<SelectListItem>? Member { get; set; }
        public IEnumerable<SelectListItem>? Chat { get; set; }

        protected override async Task LoadKeysValues() {
            List<TableRow> users = await _databaseManager.ReadAll("Users");
            List<TableRow> chats = await _databaseManager.ReadAll("Chats");

            Member = users.GenerateSelectFromRows("username");
            Chat = chats.GenerateSelectFromRows("name");
        }
    }
}
