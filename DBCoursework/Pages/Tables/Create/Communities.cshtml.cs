using DBCoursework.BaseModels;
using DBCoursework.Database;
using DBCoursework.Forms;
using DBCoursework.Forms.Tables;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DBCoursework.Pages.Tables.Create {
    public class CommunitiesModel(DatabaseManager databaseManager, FormsManager formsManager) : CreateTableModel<CommunitiesForm>(databaseManager, formsManager) {
        public IEnumerable<SelectListItem>? Creator { get; set; }
        public IEnumerable<SelectListItem>? Members { get; set; }

        protected override async Task LoadKeysValues() {
            List<TableRow> users = await _databaseManager.ReadAll("Users");
            Creator = users.GenerateSelectFromRows("username");
            Members = users.GenerateSelectFromRows("username");
        }

        protected override async Task UpdateManyToManyKeys() {
            await _databaseManager.UpdateManyToMany("CommunityMembers", "CommunityId", _createdObjectId, "UserId", Form.Members);
        }
    }
}
