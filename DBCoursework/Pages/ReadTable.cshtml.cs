using DBCoursework.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DBCoursework.Pages {
    public class ReadTableModel : PageModel {
        private DatabaseManager _databaseManager;

        public required IReadOnlyList<TableRow> Rows { get; set; }

        [BindProperty(SupportsGet = true)]
        public required string TableName { get; set; }

        public ReadTableModel(DatabaseManager databaseManager) {
            _databaseManager = databaseManager;
        }

        async public Task<IActionResult> OnGetAsync() {
            Rows = await _databaseManager.GetAll(TableName);
            return Page();
        }
    }
}
