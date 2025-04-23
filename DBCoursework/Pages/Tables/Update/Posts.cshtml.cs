using DBCoursework.BaseModels;
using DBCoursework.Database;
using DBCoursework.Forms;
using DBCoursework.Forms.Tables;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DBCoursework.Pages.Tables.Update {
    public class PostsModel(DatabaseManager databaseManager, FormsManager formsManager) : UpdateTableModel<PostsForm>(databaseManager, formsManager) {
        public IEnumerable<SelectListItem>? Author { get; set; }
        public IEnumerable<SelectListItem>? Community { get; set; }
        public IEnumerable<SelectListItem>? Likes { get; set; }

        public async override Task<IActionResult> OnGetAsync(int id) {
            await base.OnGetAsync(id);
            Form.Likes = await _databaseManager.ReadManyToManyById("LikesToPosts", "PostId", id, "UserId");
            return Page();
        }

        protected override async Task LoadKeysValues() {
            List<TableRow> users = await _databaseManager.ReadAll("Users");
            List<TableRow> communities = await _databaseManager.ReadAll("Communities");

            Author = users.GenerateSelectFromRows("username");
            Likes = users.GenerateSelectFromRows("username");
            Community = communities.GenerateSelectFromRows("name");
        }

        protected override async Task UpdateManyToManyKeys() {
            await _databaseManager.UpdateManyToMany("LikesToPosts", "PostId", _updatedObjectId, "UserId", Form.Likes);
        }
    }
}
