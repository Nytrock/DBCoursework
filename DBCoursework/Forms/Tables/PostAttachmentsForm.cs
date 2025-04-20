using System.ComponentModel.DataAnnotations;

namespace DBCoursework.Forms.Tables {
    public class PostAttachmentsForm : BaseForm {
        [Required]
        [Display(Name = "Пост:")]
        public int PostId { get; set; }

        [Required]
        [Display(Name = "Путь к файлу:")]
        [DataType(DataType.Url)]
        public string? FilePath { get; set; }
    }
}
