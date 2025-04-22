using DBCoursework.BaseModels;
using DBCoursework.Database;
using DBCoursework.Forms;
using DBCoursework.Forms.Tables;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DBCoursework.Pages.Tables.Update {
    public class CommunitiesModel : UpdateTableModel<CommunitiesForm> {
        public IEnumerable<SelectListItem>? Creator { get; set; }
        public IEnumerable<SelectListItem>? Members { get; set; }

        public CommunitiesModel(DatabaseManager databaseManager, FormsManager formsManager) : base(databaseManager, formsManager) { }

        public async override Task<IActionResult> OnGetAsync(int id) {
            await base.OnGetAsync(id);

            List<TableRow> users = await _databaseManager.ReadAll("Users");
            Creator = users.GenerateSelectFromRows("username");
            Members = users.GenerateSelectFromRows("username");
            Form.Members = await _databaseManager.ReadManyToManyById("CommunityMembers", "CommunityId", id, "UserId");

            return Page();
        }

        public async override Task<IActionResult> OnPostAsync(int id) {
            if (!ModelState.IsValid)
                return Page();

            IActionResult redirect = await base.OnPostAsync(id);
            _databaseManager.UpdateManyToMany("CommunityMembers", "CommunityId", id, "UserId", Form.Members);
            return redirect;
        }
    }
}
