using System.ComponentModel.DataAnnotations;

namespace DBCoursework.Forms.Tables {
    public class UserMessagesForm : BaseForm {
        [Required]
        [Display(Name = "Отправитель:")]
        public int SenderId { get; set; }

        [Required]
        [Display(Name = "Получатель:")]
        public int ReceiverId { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Сообщение:")]
        public string? Message { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Время создания:")]
        public DateTime CreationTime { get; set; }
    }
}
