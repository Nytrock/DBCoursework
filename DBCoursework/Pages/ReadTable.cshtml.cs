using DBCoursework.Database;
using DBCoursework.Utils;
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

        public async Task<IActionResult> OnGetAsync() {
            Rows = await _databaseManager.ReadAll(TableName);
            await LoadForeignKeys();
            return Page();
        }

        private async Task LoadForeignKeys() {
            List<TableRow> users, communities, posts, chats;
            switch (TableName) {
                case "Users":
                    foreach (var row in Rows)
                        row.PopColumn("password");
                    break;
                case "UserLogins":
                    users = await _databaseManager.ReadAll("Users");
                    foreach (var row in Rows) {
                        object userId = row.PopColumn("user_id");
                        string? userName = Convert.ToString(users.GetColumnByOtherColumn("username", "id", userId));
                        row.SetColumn("user", userName);
                    }
                    break;
                case "Friends":
                    users = await _databaseManager.ReadAll("Users");
                    foreach (var row in Rows) {
                        object userId = row.PopColumn("user_id");
                        object friendId = row.PopColumn("friend_id");

                        string? userName = Convert.ToString(users.GetColumnByOtherColumn("username", "id", userId));
                        string? friendName = Convert.ToString(users.GetColumnByOtherColumn("username", "id", friendId));

                        row.SetColumn("friend", friendName);
                        row.SetColumn("user", userName);
                    }
                    break;
                case "Communities":
                case "Chats":
                    users = await _databaseManager.ReadAll("Users");
                    foreach (var row in Rows) {
                        object creatorId = row.PopColumn("creator_id");
                        string? creatorName = Convert.ToString(users.GetColumnByOtherColumn("username", "id", creatorId));
                        row.SetColumn("creator", creatorName);
                    }
                    break;
                case "Posts":
                    users = await _databaseManager.ReadAll("Users");
                    communities = await _databaseManager.ReadAll("Communities");
                    foreach (var row in Rows) {
                        object authorId = row.PopColumn("author_id");
                        object communityId = row.PopColumn("community_id");

                        string? authorName = Convert.ToString(users.GetColumnByOtherColumn("username", "id", authorId));
                        string? communityName = Convert.ToString(communities.GetColumnByOtherColumn("name", "id", communityId));

                        row.SetColumn("author", authorName);
                        row.SetColumn("community", communityName);
                    }
                    break;
                case "PostAttachments":
                    posts = await _databaseManager.ReadAll("Posts");
                    foreach (var row in Rows) {
                        object postId = row.PopColumn("post_id");
                        string? text = Convert.ToString(posts.GetColumnByOtherColumn("text", "id", postId));
                        row.SetColumn("post", text.CutString(50));
                    }
                    break;
                case "Comments":
                    users = await _databaseManager.ReadAll("Users");
                    posts = await _databaseManager.ReadAll("Posts");
                    foreach (var row in Rows) {
                        object userId = row.PopColumn("user_id");
                        object postId = row.PopColumn("post_id");

                        string? userName = Convert.ToString(users.GetColumnByOtherColumn("username", "id", userId));
                        string? postText = Convert.ToString(posts.GetColumnByOtherColumn("text", "id", postId));

                        row.SetColumn("post", postText.CutString(50));
                        row.SetColumn("user", userName);
                    }
                    break;
                case "ChatMessages":
                    users = await _databaseManager.ReadAll("Users");
                    chats = await _databaseManager.ReadAll("Chats");
                    foreach (var row in Rows) {
                        object userId = row.PopColumn("user_id");
                        object chatId = row.PopColumn("chat_id");

                        string? userName = Convert.ToString(users.GetColumnByOtherColumn("username", "id", userId));
                        string? chatName = Convert.ToString(chats.GetColumnByOtherColumn("name", "id", chatId));

                        row.SetColumn("chat", chatName);
                        row.SetColumn("user", userName);
                    }
                    break;
                case "UserMessages":
                    users = await _databaseManager.ReadAll("Users");
                    foreach (var row in Rows) {
                        object senderId = row.PopColumn("sender_id");
                        object receiverId = row.PopColumn("receiver_id");

                        string? senderName = Convert.ToString(users.GetColumnByOtherColumn("username", "id", senderId));
                        string? receiverName = Convert.ToString(users.GetColumnByOtherColumn("username", "id", receiverId));

                        row.SetColumn("receiver", senderName);
                        row.SetColumn("sender", receiverName);
                    }
                    break;
            }
        }
    }
}
