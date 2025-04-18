using DBCoursework.Database;
using DBCoursework.Forms;
using DBCoursework.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DBCoursework.BaseModels {
    public class CreateTableModel<T> : PageModel where T : BaseForm {
        private readonly DatabaseManager _databaseManager;
        private readonly FormsManager _formsManager;
        private readonly string _tableName;

        [BindProperty]
        public required T Form { get; set; }

        public CreateTableModel(DatabaseManager databaseManager, FormsManager formsManager) {
            _databaseManager = databaseManager;
            _formsManager = formsManager;
            _tableName = typeof(T).Name.Replace("Form", "");
        }

        public void OnGet() {
            Form = _formsManager.GetFormForTable(_tableName) as T;
        }

        async public Task<IActionResult> OnPostAsync() {
            TableRow tableRow = new();
            Form.SetDataToTableRow(tableRow, typeof(T));
            await _databaseManager.Insert(_tableName, tableRow);
            return Redirect(this.GetTablePageUrl(_tableName));
        }
    }
}
