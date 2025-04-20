using System.ComponentModel.DataAnnotations;

namespace DBCoursework.Forms.Tables {
    public class FriendsForm : BaseForm {
        [Required]
        [Display(Name = "Пользователь:")]
        public int UserId { get; set; }

        [Required]
        [Display(Name = "Друг:")]
        public int FriendId { get; set; }

        [StringLength(100)]
        [Display(Name = "Заметка:")]
        public string? Note { get; set; }
    }
}
