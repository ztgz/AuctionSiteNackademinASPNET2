using System.ComponentModel.DataAnnotations;

namespace Models.ViewModels
{
    public class _UserRegister
    {
        [Required(ErrorMessage = "Epost är obligatoriskt")]
        [EmailAddress]
        [Display(Name = "Epost")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Lösenord är obligatoriskt")]
        [DataType(DataType.Password)]
        [Display(Name = "Lösenord")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Bekräfta lösenord")]
        [Compare("Password", ErrorMessage = "Lösenorden matchar inte")]
        public string ConfirmPassword { get; set; }
    }
}
    