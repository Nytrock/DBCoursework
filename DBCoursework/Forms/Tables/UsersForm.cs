using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DBCoursework.Forms.Tables {
    public class UsersForm : BaseForm {
        [StringLength(100)]
        [Required]
        [Display(Name = "Имя:")]
        public string? Username { get; set; }

        [StringLength(50)]
        [Display(Name = "Ссылка:")]
        public string? Slug { get; set; }

        [StringLength(50)]
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль:")]
        public string? Password { get; set; }

        [Display(Name = "Картинка:")]
        public string? Image { get; set; }

        [DataType(DataType.Date)]
        [DefaultValue(typeof(DateTime), "2025-01-01 00:00:00")]
        [Display(Name = "Дата рождения:")]
        public DateTime Birthday { get; set; }

        [EmailAddress]
        [StringLength(100)]
        [Required]
        [Display(Name = "Почта:")]
        public string? Email { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(200)]
        [Display(Name = "Описание:")]
        public string? Description { get; set; }

        [Phone]
        [StringLength(20)]
        [Display(Name = "Номер телефона:")]
        public string? PhoneNumber { get; set; }
    }
}
