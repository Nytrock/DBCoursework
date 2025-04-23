using DBCoursework.Database;
using DBCoursework.Forms;
using DBCoursework.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DBCoursework.BaseModels {
    public class CreateTableModel<T> : PageModel where T : BaseForm {
        protected readonly DatabaseManager _databaseManager;
        private readonly FormsManager _formsManager;
        private readonly string _originalTableName;
        private readonly string _snakeTableName;
        protected int _createdObjectId;

        [BindProperty]
        public required T Form { get; set; }

        public CreateTableModel(DatabaseManager databaseManager, FormsManager formsManager) {
            _databaseManager = databaseManager;
            _formsManager = formsManager;
            _originalTableName = typeof(T).Name.Replace("Form", "");
            _snakeTableName = _originalTableName.ToSnakeCase();
        }

        public async virtual Task<IActionResult> OnGetAsync() {
            Form = _formsManager.GetFormForTable(_originalTableName) as T;
            await LoadKeysValues();
            return Page();
        }

        public async virtual Task<IActionResult> OnPostAsync() {
            if (!ModelState.IsValid) {
                await LoadKeysValues();
                return Page();
            }

            TableRow tableRow = new();
            Form.SetDataToTableRow(tableRow, typeof(T), true);
            _createdObjectId = await _databaseManager.Create(_snakeTableName, tableRow);
            return Redirect(PageUtils.GetTablePageUrl(this, _originalTableName));
        }

        protected async virtual Task LoadKeysValues() { }
        protected async virtual Task UpdateManyToManyKeys() { }
    }
}
