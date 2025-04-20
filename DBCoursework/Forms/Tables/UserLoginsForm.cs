using System.ComponentModel.DataAnnotations;

namespace DBCoursework.Forms.Tables {
    public class UserLoginsForm : BaseForm {
        [Required]
        [Display(Name = "Пользователь:")]
        public int UserId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Дата входа:")]
        public DateTime LoginDate { get; set; }
    }
}
