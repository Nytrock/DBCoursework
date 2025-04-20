using System.ComponentModel.DataAnnotations;

namespace DBCoursework.Forms.Tables {
    public class CommentsForm : BaseForm {
        [Required]
        [Display(Name = "Автор:")]
        public int UserId { get; set; }

        [Required]
        [Display(Name = "Пост:")]
        public int PostId { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Текст:")]
        public string? Text { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Время создания:")]
        public DateTime CreationTime { get; set; }


        [Display(Name = "Лайки:")]
        public IEnumerable<int>? Likes { get; set; }
    }
}
