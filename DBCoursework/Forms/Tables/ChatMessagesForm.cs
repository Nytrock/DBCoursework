using System.ComponentModel.DataAnnotations;

namespace DBCoursework.Forms.Tables {
    public class ChatMessagesForm : BaseForm {
        [Required]
        [Display(Name = "Пользователь:")]
        public int UserId { get; set; }

        [Required]
        [Display(Name = "Чат:")]
        public int ChatId { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Сообщение:")]
        public string? Message { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Время создания:")]
        public DateTime CreationTime { get; set; }
    }
}
