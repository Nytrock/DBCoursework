using DBCoursework.Database;
using DBCoursework.Forms;
using DBCoursework.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DBCoursework.BaseModels {
    public class UpdateTableModel<T> : PageModel where T : BaseForm {
        protected readonly DatabaseManager _databaseManager;
        private readonly FormsManager _formsManager;
        private readonly string _originalTableName;
        private readonly string _snakeTableName;
        protected int _updatedObjectId;

        [BindProperty]
        public required T Form { get; set; }

        public UpdateTableModel(DatabaseManager databaseManager, FormsManager formsManager) {
            _databaseManager = databaseManager;
            _formsManager = formsManager;
            _originalTableName = typeof(T).Name.Replace("Form", "");
            _snakeTableName = _originalTableName.ToSnakeCase();
        }

        public async virtual Task<IActionResult> OnGetAsync(int id) {
            Form = _formsManager.GetFormForTable(_originalTableName) as T;
            await LoadKeysValues();

            TableRow row = await _databaseManager.ReadById(_snakeTableName, id);
            Form?.GetDataFromTableRow(row, typeof(T));
            return Page();
        }

        public async virtual Task<IActionResult> OnPostAsync(int id) {
            _updatedObjectId = id;
            if (!ModelState.IsValid) {
                await LoadKeysValues();
                return Page();
            }

            TableRow row = new();
            Form.SetDataToTableRow(row, typeof(T));
            await _databaseManager.Update(_snakeTableName, row, id);
            await UpdateManyToManyKeys();
            return Redirect(PageUtils.GetTablePageUrl(this, _originalTableName));
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id) {
            await _databaseManager.Delete(_snakeTableName, id);
            return Redirect(PageUtils.GetTablePageUrl(this, _originalTableName));
        }

        protected async virtual Task LoadKeysValues() { }
        protected async virtual Task UpdateManyToManyKeys() { }
    }
}
