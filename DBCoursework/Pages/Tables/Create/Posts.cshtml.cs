using DBCoursework.BaseModels;
using DBCoursework.Database;
using DBCoursework.Forms;
using DBCoursework.Forms.Tables;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DBCoursework.Pages.Tables.Create {
    public class PostsModel(DatabaseManager databaseManager, FormsManager formsManager) : CreateTableModel<PostsForm>(databaseManager, formsManager) {
        public IEnumerable<SelectListItem>? Author { get; set; }
        public IEnumerable<SelectListItem>? Community { get; set; }
        public IEnumerable<SelectListItem>? Likes { get; set; }

        protected override async Task LoadKeysValues() {
            List<TableRow> users = await _databaseManager.ReadAll("Users");
            List<TableRow> communities = await _databaseManager.ReadAll("Communities");

            Author = users.GenerateSelectFromRows("username");
            Likes = users.GenerateSelectFromRows("username");
            Community = communities.GenerateSelectFromRows("name");
        }

        protected override async Task UpdateManyToManyKeys() {
            await _databaseManager.UpdateManyToMany("LikesToPosts", "PostId", _createdObjectId, "UserId", Form.Likes);
        }
    }
}
