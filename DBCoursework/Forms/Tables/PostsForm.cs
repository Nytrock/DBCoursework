using System.ComponentModel.DataAnnotations;

namespace DBCoursework.Forms.Tables {
    public class PostsForm : BaseForm {
        [Required]
        [Display(Name = "Автор:")]
        public int AuthorId { get; set; }

        [Required]
        [Display(Name = "Сообщество:")]
        public int CommunityId { get; set; }

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
