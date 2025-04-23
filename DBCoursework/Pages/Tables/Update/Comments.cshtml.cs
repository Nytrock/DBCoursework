using DBCoursework.BaseModels;
using DBCoursework.Database;
using DBCoursework.Forms;
using DBCoursework.Forms.Tables;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DBCoursework.Pages.Tables.Update {
    public class CommentsModel(DatabaseManager databaseManager, FormsManager formsManager) : UpdateTableModel<CommentsForm>(databaseManager, formsManager) {
        public IEnumerable<SelectListItem>? Author { get; set; }
        public IEnumerable<SelectListItem>? Post { get; set; }
        public IEnumerable<SelectListItem>? Likes { get; set; }

        public async override Task<IActionResult> OnGetAsync(int id) {
            await base.OnGetAsync(id);
            Form.Likes = await _databaseManager.ReadManyToManyById("LikesToComments", "CommentId", id, "UserId");
            return Page();
        }

        protected override async Task LoadKeysValues() {
            List<TableRow> users = await _databaseManager.ReadAll("Users");
            List<TableRow> communities = await _databaseManager.ReadAll("Posts");

            Author = users.GenerateSelectFromRows("username");
            Likes = users.GenerateSelectFromRows("username");
            Post = communities.GenerateSelectFromRows("text");
        }

        protected override async Task UpdateManyToManyKeys() {
            await _databaseManager.UpdateManyToMany("LikesToComments", "CommentId", _updatedObjectId, "UserId", Form.Likes);
        }
    }
}
