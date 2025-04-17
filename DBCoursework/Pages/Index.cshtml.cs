using DBCoursework.Database;
using DBCoursework.Database.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DBCoursework.Pages {
    public class IndexModel : PageModel {
        private readonly ILogger<IndexModel> _logger;
        private readonly DatabaseManager _databaseManager;

        public IndexModel(ILogger<IndexModel> logger, DatabaseManager databaseManager) {
            _logger = logger;
            _databaseManager = databaseManager;
        }

        public async void OnGetAsync() {
            await foreach (var row in _databaseManager.GetAll<UsersModel>()) {
                Console.WriteLine(row.GetStringForInsert());
                Console.WriteLine(row.GetStringForUpdate());
            }
        }
    }
}
