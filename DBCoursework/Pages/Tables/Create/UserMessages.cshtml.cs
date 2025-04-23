using DBCoursework.BaseModels;
using DBCoursework.Database;
using DBCoursework.Forms;
using DBCoursework.Forms.Tables;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DBCoursework.Pages.Tables.Create {
    public class UserMessagesModel(DatabaseManager databaseManager, FormsManager formsManager) : CreateTableModel<UserMessagesForm>(databaseManager, formsManager) {
        public IEnumerable<SelectListItem>? Sender { get; set; }
        public IEnumerable<SelectListItem>? Receiver { get; set; }

        protected override async Task LoadKeysValues() {
            List<TableRow> users = await _databaseManager.ReadAll("Users");
            Sender = users.GenerateSelectFromRows("username");
            Receiver = users.GenerateSelectFromRows("username");
        }

        public override async Task<IActionResult> OnPostAsync() {
            if (Form.SenderId == Form.ReceiverId)
                ModelState.AddModelError("Form.ReceiverId", "ѕользователь не может писать самому себе");
            return await base.OnPostAsync();
        }
    }
}
