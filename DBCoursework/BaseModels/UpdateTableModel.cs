using DBCoursework.Database;
using DBCoursework.Forms;
using DBCoursework.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DBCoursework.BaseModels {
    public class UpdateTableModel<T> : PageModel where T : BaseForm {
        private readonly DatabaseManager _databaseManager;
        private readonly FormsManager _formsManager;
        private readonly string _tableName;

        [BindProperty]
        public required T Form { get; set; }

        public UpdateTableModel(DatabaseManager databaseManager, FormsManager formsManager) {
            _databaseManager = databaseManager;
            _formsManager = formsManager;
            _tableName = typeof(T).Name.Replace("Form", "");
        }

        async public Task<IActionResult> OnGetAsync(int id) {
            Form = _formsManager.GetFormForTable(_tableName) as T;

            TableRow row = await _databaseManager.GetById(_tableName, id);
            Form?.GetDataFromTableRow(row, typeof(T));
            return Page();
        }

        async public Task<IActionResult> OnPostAsync(int id) {
            TableRow row = new();
            Form.SetDataToTableRow(row, typeof(T));
            await _databaseManager.Update(_tableName, row, id);
            return Redirect(this.GetTablePageUrl(_tableName));
        }

        async public Task<IActionResult> OnPostDeleteAsync(int id) {
            Console.WriteLine(1);
            await _databaseManager.Delete(_tableName, id);
            return Redirect(this.GetTablePageUrl(_tableName));
        }
    }
}
