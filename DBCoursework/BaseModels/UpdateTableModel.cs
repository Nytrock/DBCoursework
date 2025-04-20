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

        async public virtual Task<IActionResult> OnGetAsync(int id) {
            Form = _formsManager.GetFormForTable(_tableName) as T;

            TableRow row = await _databaseManager.GetById(_tableName, id);
            Form?.GetDataFromTableRow(row, typeof(T));
            return Page();
        }

        async public virtual Task<IActionResult> OnPostAsync(int id) {
            if (!ModelState.IsValid)
                return Page();

            TableRow row = new();
            Form.SetDataToTableRow(row, typeof(T));
            await _databaseManager.Update(_tableName, row, id);
            return Redirect(PageUtils.GetTablePageUrl(this, _tableName));
        }

        async public virtual Task<IActionResult> OnPostDeleteAsync(int id) {
            await _databaseManager.Delete(_tableName, id);
            return Redirect(PageUtils.GetTablePageUrl(this, _tableName));
        }
    }
}
