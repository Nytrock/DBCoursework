using System.ComponentModel.DataAnnotations;

namespace DBCoursework.Forms.Tables {
    public class CommunitiesForm : BaseForm {
        [Required]
        [Display(Name = "Создатель:")]
        public int CreatorId { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Название:")]
        public string? Name { get; set; }

        [StringLength(100)]
        [Display(Name = "Ссылка:")]
        public string? Slug { get; set; }

        [Display(Name = "Картинка:")]
        [DataType(DataType.ImageUrl)]
        public string? Image { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(200)]
        [Display(Name = "Описание:")]
        public string? Description { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Дата создания:")]
        public DateTime CreationDate { get; set; }

        [Display(Name = "Подписчики:")]
        public IEnumerable<int>? Members { get; set; }
    }
}
