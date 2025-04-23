using DBCoursework.BaseModels;
using DBCoursework.Database;
using DBCoursework.Forms;
using DBCoursework.Forms.Tables;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DBCoursework.Pages.Tables.Update {
    public class PostAttachmentsModel(DatabaseManager databaseManager, FormsManager formsManager) : UpdateTableModel<PostAttachmentsForm>(databaseManager, formsManager) {
        public IEnumerable<SelectListItem>? Post { get; set; }

        protected override async Task LoadKeysValues() {
            List<TableRow> posts = await _databaseManager.ReadAll("Posts");
            Post = posts.GenerateSelectFromRows("text");
        }
    }
}
