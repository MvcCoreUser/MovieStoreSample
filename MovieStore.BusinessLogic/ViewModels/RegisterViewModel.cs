using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.BusinessLogic.ViewModels
{
    public class RegisterViewModel: AModel
    {

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name ="Адрес электронной почты")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name="Пароль")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        [Display(Name ="Подтвердить пароль")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name="Телефон")]
        public string Phone { get; set; }

        [Required]
        [Display(Name ="Краткое имя пользователя (англ. буквами)")]
        public string Name { get; set; }
    }
}
