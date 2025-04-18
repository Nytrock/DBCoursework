using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DBCoursework.Utils {
    public static class PageUtils {
        public static string GetTablePageUrl(this PageModel pageModel, string tableName) {
            string? url = pageModel.Url.Page("/ReadTable", new { TableName = tableName });
            if (url == null)
                return tableName;
            return url;
        }
    }
}
