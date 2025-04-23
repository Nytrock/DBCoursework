using DBCoursework.BaseModels;
using DBCoursework.Database;
using DBCoursework.Forms;
using DBCoursework.Forms.Tables;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DBCoursework.Pages.Tables.Update {
    public class ChatsModel : UpdateTableModel<ChatsForm> {
        public IEnumerable<SelectListItem>? Creator { get; set; }
        public IEnumerable<SelectListItem>? Members { get; set; }

        public ChatsModel(DatabaseManager databaseManager, FormsManager formsManager) : base(databaseManager, formsManager) { }

        public async override Task<IActionResult> OnGetAsync(int id) {
            await base.OnGetAsync(id);
            Form.Members = await _databaseManager.ReadManyToManyById("CommunityMembers", "CommunityId", id, "UserId");
            return Page();
        }

        protected override async Task LoadKeysValues() {
            List<TableRow> users = await _databaseManager.ReadAll("Users");
            Creator = users.GenerateSelectFromRows("username");
            Members = users.GenerateSelectFromRows("username");
        }

        protected override async Task UpdateManyToManyKeys() {
            await _databaseManager.UpdateManyToMany("CommunityMembers", "CommunityId", _updatedObjectId, "UserId", Form.Members);
        }
    }
}
