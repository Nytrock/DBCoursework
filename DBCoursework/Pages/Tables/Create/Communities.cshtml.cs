using DBCoursework.BaseModels;
using DBCoursework.Database;
using DBCoursework.Forms;
using DBCoursework.Forms.Tables;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DBCoursework.Pages.Tables.Create {
    public class CommunitiesModel : CreateTableModel<CommunitiesForm> {
        public IEnumerable<SelectListItem>? Creator { get; set; }
        public IEnumerable<SelectListItem>? Members { get; set; }

        public CommunitiesModel(DatabaseManager databaseManager, FormsManager formsManager) : base(databaseManager, formsManager) {
        }

        public async override Task<IActionResult> OnGetAsync() {
            await base.OnGetAsync();
            List<TableRow> users = await _databaseManager.ReadAll("Users");
            Creator = users.GenerateSelectFromRows("username");
            Members = users.GenerateSelectFromRows("username");
            return Page();
        }

        public async override Task<IActionResult> OnPostAsync() {
            if (!ModelState.IsValid)
                return Page();

            IActionResult redirect = await base.OnPostAsync();
            _databaseManager.UpdateManyToMany("CommunityMembers", "CommunityId", _createdObjectId, "UserId", Form.Members);
            return redirect;
        }
    }
}
