using DBCoursework.Database;
using DBCoursework.Forms;
using DBCoursework.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DBCoursework.BaseModels {
    public class CreateTableModel<T> : PageModel where T : BaseForm {
        protected readonly DatabaseManager _databaseManager;
        private readonly FormsManager _formsManager;
        private readonly string _tableName;
        protected int _createdObjectId;

        [BindProperty]
        public required T Form { get; set; }

        public CreateTableModel(DatabaseManager databaseManager, FormsManager formsManager) {
            _databaseManager = databaseManager;
            _formsManager = formsManager;
            _tableName = typeof(T).Name.Replace("Form", "");
        }

        public async virtual Task<IActionResult> OnGetAsync() {
            Form = _formsManager.GetFormForTable(_tableName) as T;
            return Page();
        }

        public async virtual Task<IActionResult> OnPostAsync() {
            if (!ModelState.IsValid)
                return Page();

            TableRow tableRow = new();
            Form.SetDataToTableRow(tableRow, typeof(T), true);
            _createdObjectId = await _databaseManager.Create(_tableName, tableRow);
            return Redirect(PageUtils.GetTablePageUrl(this, _tableName));
        }
    }
}
