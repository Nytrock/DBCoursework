using DBCoursework.Forms.Tables;

namespace DBCoursework.Forms {
    public class FormsManager {
        public BaseForm? GetFormForTable(string tableName) {
            switch (tableName) {
                case "Users":
                    return new UsersForm();
                case "UserLogins":
                    return new UserLoginsForm();
                case "Friends":
                    return new FriendsForm();
                case "Communities":
                    return new CommunitiesForm();
                case "Posts":
                    return new PostsForm();
                case "PostAttachments":
                    return new PostAttachmentsForm();
                case "Comments":
                    return new CommentsForm();
                case "Chats":
                    return new ChatsForm();
                case "ChatMessages":
                    return new ChatMessagesForm();
                case "UserMessages":
                    return new UserMessagesForm();
                default:
                    return null;
            }
        }
    }
}
