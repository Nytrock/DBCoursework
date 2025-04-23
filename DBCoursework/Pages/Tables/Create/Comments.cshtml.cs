using DBCoursework.BaseModels;
using DBCoursework.Database;
using DBCoursework.Forms;
using DBCoursework.Forms.Tables;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DBCoursework.Pages.Tables.Create {
    public class CommentsModel(DatabaseManager databaseManager, FormsManager formsManager) : CreateTableModel<CommentsForm>(databaseManager, formsManager) {
        public IEnumerable<SelectListItem>? Author { get; set; }
        public IEnumerable<SelectListItem>? Post { get; set; }
        public IEnumerable<SelectListItem>? Likes { get; set; }

        protected override async Task LoadKeysValues() {
            List<TableRow> users = await _databaseManager.ReadAll("Users");
            List<TableRow> posts = await _databaseManager.ReadAll("Posts");

            Author = users.GenerateSelectFromRows("username");
            Likes = users.GenerateSelectFromRows("username");
            Post = posts.GenerateSelectFromRows("text");
        }

        protected override async Task UpdateManyToManyKeys() {
            await _databaseManager.UpdateManyToMany("LikesToComments", "CommentId", _createdObjectId, "UserId", Form.Likes);
        }
    }
}
