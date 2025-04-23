using DBCoursework.BaseModels;
using DBCoursework.Database;
using DBCoursework.Forms;
using DBCoursework.Forms.Tables;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DBCoursework.Pages.Tables.Create {
    public class FriendsModel(DatabaseManager databaseManager, FormsManager formsManager) : CreateTableModel<FriendsForm>(databaseManager, formsManager) {
        public IEnumerable<SelectListItem>? OriginalUser { get; set; }
        public IEnumerable<SelectListItem>? Friend { get; set; }

        public override async Task<IActionResult> OnPostAsync() {
            if (Form.UserId == Form.FriendId)
                ModelState.AddModelError("Form.FriendId", "ѕользователь не может быть другом самому себе");
            return await base.OnPostAsync();
        }

        protected override async Task LoadKeysValues() {
            List<TableRow> users = await _databaseManager.ReadAll("Users");
            OriginalUser = users.GenerateSelectFromRows("username");
            Friend = users.GenerateSelectFromRows("username");
        }
    }
}
