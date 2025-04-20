using System.ComponentModel.DataAnnotations;

namespace DBCoursework.Forms.Tables {
    public class ChatsForm : BaseForm {
        [Required]
        [Display(Name = "Создатель:")]
        public int CreatorId { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Название:")]
        public string? Name { get; set; }

        [Display(Name = "Изображение:")]
        [DataType(DataType.ImageUrl)]
        public string? Image { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Дата создания:")]
        public DateTime CreationDate { get; set; }

        [Display(Name = "Участники:")]
        public IEnumerable<int>? Members { get; set; }
    }
}
